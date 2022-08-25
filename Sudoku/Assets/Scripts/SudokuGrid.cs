using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_pos = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;
    public float square_gap = 0.1f;
    public Color line_highlight_colour = Color.red;

    private List<GameObject> grid_squares = new List<GameObject>();
    private int selected_grid_data;

    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("this game object needs to have a grid square script attached.");

        create_grid();

        if (GameSettings.instance.get_continue_prev_game())
            set_grid_from_file();
        else
            set_grid_num(GameSettings.instance.get_game_mode());

    }
    private void set_grid_from_file()
    {
        string level = GameSettings.instance.get_game_mode();
        selected_grid_data = Config.read_level();
        var data = Config.read_grid_data();

        set_grid_square_data(data);
        set_grid_notes(Config.get_grid_notes());
    }

    private void set_grid_notes(Dictionary<int, List<int>> notes)
    {
        foreach (var note in notes)
        {
            grid_squares[note.Key].GetComponent<GridSquare>().set_grid_notes(note.Value);
        }
    }

    private void create_grid()
    {
        spawn_grid_squares();
        set_square_pos();
    }

    private void spawn_grid_squares()
    {
        // 
        int square_index = 0;
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                grid_squares.Add(Instantiate(grid_square) as GameObject); // figure 
                grid_squares[grid_squares.Count - 1].GetComponent<GridSquare>().set_square_index(square_index);
                grid_squares[grid_squares.Count - 1].transform.parent = this.transform; // figure out what this does 
                grid_squares[grid_squares.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);

                square_index++;
            }
        }
    }

    private void set_square_pos()
    {
        var square_rect = grid_squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 square_gap_num = new Vector2(0.0f, 0.0f);
        bool row_move = false;

        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_num = 0;
        int row_num = 0;

        foreach(GameObject square in grid_squares)
        {
            if(column_num + 1 > columns)
            {
                row_num++;
                column_num = 0;
                square_gap_num.x = 0;
                row_move = false; //indicates if row has been switched 
            }
            var pos_x_offset = offset.x * column_num + (square_gap_num.x * square_gap);
            var pos_y_offset = offset.y * row_num + (square_gap_num.y * square_gap);

            if (column_num > 0 && column_num % 3 == 0) //check for every third column since each mini square needs a larger gap
            {
                square_gap_num.x++;
                pos_x_offset += square_gap;
            }

            if (row_num > 0 && row_num % 3 == 0 && row_move == false)
            {
                row_move = true;
                square_gap_num.y++;
                pos_y_offset += square_gap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_pos.x + pos_x_offset, start_pos.y - pos_y_offset);
            column_num++;
        }
    }

    private void set_grid_num(string level)
    {
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][selected_grid_data];

        set_grid_square_data(data);
        //foreach (var square in grid_squares)
        //{
        //    square.GetComponent<GridSquare>().set_num(Random.Range(0, 10));
        //}
    }
    
    private void set_grid_square_data(SudokuData.sudoku_board_data data)
    {
        for (int index = 0; index < grid_squares.Count; index++)
        {
            grid_squares[index].GetComponent<GridSquare>().set_num(data.unsolved_data[index]);
            grid_squares[index].GetComponent<GridSquare>().set_correct_num(data.solved_data[index]);
            grid_squares[index].GetComponent<GridSquare>().set_default_values(data.unsolved_data[index] != 0 && data.unsolved_data[index] == data.solved_data[index]);
        }
    }
    private void OnEnable()
    {
        GameEvents.on_square_selected += on_square_selected;
        GameEvents.on_update_square_num += check_for_completion;
    }
    private void OnDisable()
    {
        GameEvents.on_square_selected -= on_square_selected;
        GameEvents.on_update_square_num -= check_for_completion;

        var solved_data = SudokuData.Instance.sudoku_game[GameSettings.instance.get_game_mode()][selected_grid_data].solved_data;
        int[] unsolved_data = new int[81];

        Dictionary<string, List<string>> grid_notes = new Dictionary<string, List<string>>();

        for (int i = 0; i < grid_squares.Count; i++)
        {
            var comp = grid_squares[i].GetComponent<GridSquare>();
            unsolved_data[i] = comp.get_square_num();
            string key = "square_note:" + i.ToString();
            grid_notes.Add(key, comp.get_square_note());
        }

        SudokuData.sudoku_board_data current_game_data = new SudokuData.sudoku_board_data(unsolved_data, solved_data);

        if (GameSettings.instance.get_exit_after_won() == false) //if you win, don't save the data
        {
            Config.save_board_data(current_game_data, GameSettings.instance.get_game_mode(), selected_grid_data, Errors.instance.get_error_num(), grid_notes);
        }
        else
        {
            Config.delete_data_file();
        }
    }

    private void set_square_colour(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = grid_squares[index].GetComponent<GridSquare>();
            if (comp.has_wrong_value_func() == false && comp.is_selected() == false)
            {
                comp.set_colour(col);
            }
        }
    }

    public void on_square_selected(int square_index)
    {
        var hor_line = LineHighlights.instance.get_horizontal_line(square_index);
        var vert_line = LineHighlights.instance.get_vertical_line(square_index);
        var square = LineHighlights.instance.get_square(square_index);

        set_square_colour(LineHighlights.instance.get_all_square_indexes(), Color.white);
        set_square_colour(hor_line, line_highlight_colour);
        set_square_colour(vert_line, line_highlight_colour);
        set_square_colour(square, line_highlight_colour);
    }

    private void check_for_completion(int num)
    {
        foreach (var square in grid_squares) //search through board to see if everything is correct 
        {
            var comp = square.GetComponent<GridSquare>();

            if (comp.is_correct_num() == false)
            {
                return;
            }
        }
        GameEvents.on_board_complete_method();
    }

    public void solve_sudoku()
    {
        foreach (var square in grid_squares)
        {
            var comp = square.GetComponent<GridSquare>();

            comp.set_correct_num(); //display correct board 

        }

        check_for_completion(0);
    }
}

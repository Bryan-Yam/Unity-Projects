using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Config : MonoBehaviour
{
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    private static string directory = Application.persistentDataPath;
#else
    private static string directory = Directory.GetCurrentDirectory();
#endif 

    private static string file = @"\board_data.ini";
    private static string path = directory + file;

    public static void delete_data_file()
    {
        File.Delete(path);
    }

    public static void save_board_data(SudokuData.sudoku_board_data board_data, string level, int board_index, int error_num, Dictionary<string, List<string>> grid_notes)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string current_time = "#time:" + Clock.get_current_time();
        string level_string = "#level:" + level;
        string error_number = "#errors:" + error_num;
        string board_index_string = "#board_index:" + board_index.ToString();
        string unsolved_string = "#unsolved:";
        string solved_string = "#solved:";

        foreach (var unsolved_data in board_data.unsolved_data)
        {
            unsolved_string += unsolved_data.ToString() + ",";
        }

        foreach (var solved_data in board_data.solved_data)
        {
            solved_string += solved_data.ToString() + ",";
        }

        writer.WriteLine(current_time);
        writer.WriteLine(level_string);
        writer.WriteLine(error_number);
        writer.WriteLine(board_index_string);
        writer.WriteLine(unsolved_string);
        writer.WriteLine(solved_string);

        foreach (var square in grid_notes)
        {
            string square_string = "#" + square.Key + ",";
            bool save = false;

            foreach (var note in square.Value)
            {
                if (note != " ")
                {
                    square_string += note + ",";
                    save = true;
                }
            }

            if (save) //only save notes if there are notes placed 
                writer.WriteLine(square_string);
        }

        writer.Close();
    }

    public static Dictionary<int , List<int>> get_grid_notes()
    {
        Dictionary<int, List<int>> grid_notes = new Dictionary<int, List<int>>();
        string line;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#square_note")
            {
                int square_index = -1;
                List<int> notes = new List<int>();
                int.TryParse(word[1], out square_index); //int to string 
                string[] substring = Regex.Split(word[2], ","); //split word[2] by commas 

                foreach ( var note in substring)
                {
                    int note_num = -1;
                    int.TryParse(note, out note_num);
                    if (note_num > 0)
                        notes.Add(note_num);
                }

                grid_notes.Add(square_index, notes);
            }
        }

        file.Close();

        return grid_notes;
    }

    public static string read_board_level()
    {
        string line;
        string level = "";
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#level")
            {
                level = word[1];
            }
        }

        file.Close();

        return level;
    }

    public static SudokuData.sudoku_board_data read_grid_data()
    {
        string line;
        StreamReader file = new StreamReader(path);

        int[] unsolved_data = new int[81];
        int[] solved_data = new int[81];

        int unsolved_index = 0;
        int solved_index = 0;

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "unsolved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach(var value in substrings)
                {
                    int square_num = -1;
                    if (int.TryParse(value, out square_num))
                    {
                        unsolved_data[unsolved_index] = square_num; //input square number into unsolved data array
                        unsolved_index++; //move to next index 
                    }
                }
            }

            if (word[0] == "solved")
            {
                string[] substrings = Regex.Split(word[1], ",");

                foreach (var value in substrings)
                {
                    int square_num = -1;
                    if (int.TryParse(value, out square_num))
                    {
                        solved_data[solved_index] = square_num; //input square number into unsolved data array
                        solved_index++; //move to next index 
                    }
                }
            }
        }
        file.Close();

        return new SudokuData.sudoku_board_data(unsolved_data, solved_data);
    }

    public static int read_level()
    {
        int level = -1;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');

            if (word[0] == "#board_index")
            {
                int.TryParse(word[1], out level);
            }
        }

        file.Close();

        return level;
    }

    public static float read_game_time()
    {
        float time = -1.0f;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');

            if (word[0] == "#time")
            {
                float.TryParse(word[1], out time);
            }
        }

        file.Close();

        return time;
    }

    public static int read_error_num()
    {
        int error = 0;
        string line;
        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');

            if (word[0] == "#errors")
            {
                int.TryParse(word[1], out error);
            }
        }

        file.Close();

        return error;
    }

    public static bool game_data_exist()
    {
        return File.Exists(path);
    }
}

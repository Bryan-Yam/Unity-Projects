using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHighlights : MonoBehaviour
{
    public static LineHighlights instance;

    private int[,] line_data = new int[9, 9]
    {
        {0,1,2,3,4,5,6,7,8},
        {9,10,11,12,13,14,15,16,17},
        {18,19,20,21,22,23,24,25,26},
        {27,28,29,30,31,32,33,34,35},
        {36,37,38,39,40,41,42,43,44},
        {45,46,47,48,49,50,51,52,53},
        {54,55,56,57,58,59,60,61,62},
        {63,64,65,66,67,68,69,70,71},
        {72,73,74,75,76,77,78,79,80}
    };

    private int[] line_data_flat = new int[81]
    {
        0,1,2,3,4,5,6,7,8,
        9,10,11,12,13,14,15,16,17,
        18,19,20,21,22,23,24,25,26,
        27,28,29,30,31,32,33,34,35,
        36,37,38,39,40,41,42,43,44,
        45,46,47,48,49,50,51,52,53,
        54,55,56,57,58,59,60,61,62,
        63,64,65,66,67,68,69,70,71,
        72,73,74,75,76,77,78,79,80
    };

    private int[,] square_data = new int[9, 9]
    {
        {0,1,2,9,10,11,18,19,20},
        {3,4,5,12,13,14,21,22,23},
        {6,7,8,15,16,17,24,25,26},
        {27,28,29,36,37,38,45,46,47},
        {30,31,32,39,40,41,48,49,50},
        {33,34,35,42,43,44,51,52,53},
        {54,55,56,63,64,65,72,73,74},
        {57,58,59,66,67,68,75,76,77},
        {60,61,62,69,70,71,78,79,80}
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private (int,int) get_square_pos(int square_index)
    {
        int row_pos = -1;
        int col_pos = -1;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (line_data[row,col] == square_index)
                {
                    row_pos = row;
                    col_pos = col;
                    return (row_pos, col_pos);
                }
            }
        }
        return (row_pos, col_pos);
    }

    public int[] get_horizontal_line(int square_index)
    {
        int[] line = new int[9];

        var square_pos_row = get_square_pos(square_index).Item1;

        for (int index = 0; index < 9; index++)
        {
            line[index] = line_data[square_pos_row, index]; //copy all elements in the row
        }
        return line;
    }

    public int[] get_vertical_line(int square_index)
    {
        int[] line = new int[9];

        var square_pos_col = get_square_pos(square_index).Item2;

        for (int index = 0; index < 9; index++)
        {
            line[index] = line_data[index, square_pos_col];
        }
        return line;
    }

    public int[] get_square(int square_index)
    {
        int[] line = new int[9];

        int row_pos = -1;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (square_data[row, col] == square_index)
                {
                    row_pos = row; //get row since square data already has the correct squares 
                }
            }
        }

        for(int index = 0; index < 9; index++)
        {
            line[index] = square_data[row_pos, index]; //copy over the respective square from the row found 
        }

        return line;
    }

    public int[] get_all_square_indexes()
    {
        return line_data_flat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void update_square_num(int number);

    public static event update_square_num on_update_square_num;

    public static void update_square_num_method(int number)
    {
        if (on_update_square_num != null)
            on_update_square_num(number);
    }

    public delegate void square_selected(int square_index);

    public static event square_selected on_square_selected;

    public static void square_selected_method(int square_index)
    {
        if (on_square_selected != null)
            on_square_selected(square_index);
    }

    public delegate void wrong_num();

    public static event wrong_num on_wrong_num;

    public static void on_wrong_num_method()
    {
        if (on_wrong_num != null)
            on_wrong_num();
    }

    public delegate void game_over();

    public static event game_over on_game_over;

    public static void on_game_over_method()
    {
        if (on_game_over != null)
            on_game_over();
    }

    public delegate void notes_active(bool active);

    public static event notes_active on_notes_active;

    public static void on_notes_active_method(bool active)
    {
        if (on_notes_active != null)
            on_notes_active(active);
    }

    public delegate void erase_num();

    public static event erase_num on_erase_num;

    public static void on_erase_num_method()
    {
        if (on_erase_num != null)
            on_erase_num();
    }

    public delegate void board_complete();

    public static event board_complete on_board_complete;
    public static void on_board_complete_method()
    {
        if (on_board_complete != null)
            on_board_complete();
    }
}

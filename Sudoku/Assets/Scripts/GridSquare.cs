using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;

    public List<GameObject> number_notes;

    private bool note_active;

    private int num = 0;
    private int correct_num = 0;

    private bool selection = false;
    private int square_index = -1;
    private bool default_value = false;
    private bool has_wrong_value = false;

    public int get_square_num()
    {
        return num;
    }
    public bool is_correct_num()
    {
        return num == correct_num;
    }

    public bool has_wrong_value_func()
    {
        return has_wrong_value;
    }

    public void set_default_values(bool has_default_value)
    {
        default_value = has_default_value;
    }

    public bool get_default_values()
    {
        return default_value;
    }

    public bool is_selected()
    {
        return selection;
    }

    public void set_square_index(int index)
    {
        square_index = index;
    }

    public void set_correct_num(int number)
    {
        correct_num = number;
        has_wrong_value = false;
    }

    public void set_correct_num()
    {
        num = correct_num;
        set_note_num_value(0);
        display_text();
    }
    void Start()
    {
        selection = false;
        note_active = false;

        set_note_num_value(0); //makes sure notes arent shown at the beginning of the game
    }

    public List<string> get_square_note() //helper function 
    {
        List<string> notes = new List<string>();

        foreach(var num in number_notes)
        {
            notes.Add(num.GetComponent<Text>().text);
        }

        return notes;
     }

    private void set_clear_empty_notes()
    {
        foreach (var num in number_notes)
        {
            if (num.GetComponent<Text>().text == "0")
                num.GetComponent<Text>().text = " ";
        }
    }

    private void set_note_num_value(int value)
    {
        foreach (var num in number_notes)
        {
            if (value <= 0)
                num.GetComponent<Text>().text = " ";
            else
                num.GetComponent<Text>().text = value.ToString();
        }
    }    

    private void set_notes_single_num_val(int value, bool force_update = false)
    {
        if (note_active == false && force_update == false)
            return;

        if (value <= 0)
            number_notes[value - 1].GetComponent<Text>().text = " ";
        else
        {
            if (number_notes[value - 1].GetComponent<Text>().text == " " || force_update)
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
            else
                number_notes[value - 1].GetComponent<Text>().text = " ";
        }
    }

    public void set_grid_notes(List<int> notes)
    {
        foreach (var num in notes)
        {
            set_notes_single_num_val(num, true);
        }
    }

    public void on_notes_active(bool active)
    {
        note_active = active;
    }

    public void display_text()
    {
        if (num <= 0)
            number_text.GetComponent<Text>().text = " ";
        else
            number_text.GetComponent<Text>().text = num.ToString();
    }

    public void set_num(int number)
    {
        num = number;
        display_text();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selection = true;
        GameEvents.square_selected_method(square_index);
    }

    public void OnSubmit(BaseEventData eventData)
    {
       
    }
    private void OnEnable()
    {
        GameEvents.on_update_square_num += on_set_number;
        GameEvents.on_square_selected += on_square_selected;
        GameEvents.on_notes_active += on_notes_active;
        GameEvents.on_erase_num += on_erase_num;
    }
    private void OnDisable()
    {
        GameEvents.on_update_square_num -= on_set_number;
        GameEvents.on_square_selected -= on_square_selected;
        GameEvents.on_erase_num -= on_erase_num;
    }

    public void on_erase_num()
    {
        if (selection && !default_value)
        {
            num = 0;
            has_wrong_value = false;
            set_colour(Color.white);
            set_note_num_value(0);
            display_text();
        }
    }

    public void on_set_number(int number)
    {
        if (selection && default_value == false)
        {
            if (note_active == true && has_wrong_value == false)
            {
                set_notes_single_num_val(number);
            }
            else if (note_active == false)
            {
                set_note_num_value(0); //clears the notes when real number is being placed 
                set_num(number);

                if (num != correct_num)
                {
                    has_wrong_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;

                    GameEvents.on_wrong_num_method();
                }
                else
                {
                    has_wrong_value = false;
                    default_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                }
            }
        }
    }

    public void on_square_selected(int s_index)
    {
        if (square_index != s_index)
        {
            selection = false;
        }
    }

    public void set_colour(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }
}

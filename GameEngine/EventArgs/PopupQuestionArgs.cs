using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GameEngine.EventArgs
{
    public class PopupQuestionArgs : System.EventArgs
    {

        public bool ResetQuestion()
        {
            return MessageBox.Show("Are you sure you wish to reset? This will Delete all current Progress and start again.",
            "Reset Game",
            MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
        public bool ExitQuestion()
        {
            return MessageBox.Show("Are you sure you wish to Exit? This will Delete all unsaved Progress.",
            "Exit Game",
            MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
        public bool EndNewChar()
        {
            return MessageBox.Show("Are you sure you wish to Go back and Cancel Character Creation? This will Delete all current Progress and start again.",
            "Cancel Create Character?",
            MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public bool CharacterCreateContinue(String x)
        {
            return MessageBox.Show("You have Selected: " + x + ".  Would you like to continue?",
            "Continue?",
            MessageBoxButtons.YesNo) == DialogResult.Yes;
        }



    }
}

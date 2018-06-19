using System;
using System.Linq;
using System.Windows.Forms;

namespace CSharpCalculator
{
    public partial class Form1 : Form
    {

        // The first number for the operation
        decimal firstNumber = 0;
        // The second number for the operation
        decimal secondNumber = 0;
        // Vareiable to store the current number in memory
        decimal memoryNumber = 0;
        // string containing the symbol for the current operation
        string operation = "";

        // state 0 = no numbers or first number being entered
        // state 1 = first number and an operator used
        // state 2 = first number and an operator used and second number
        // state 3 = first and second numbers with an operator and equals pressed
        // state 9 = error
        int state = 0;

        public Form1()
        {
            InitializeComponent();

            // Set the intial display as "0"
            ScreenTb.Text = "0";
        }

        // Number Method
        private void NumberButtonPressed(int number)
        {
            switch (state)
            {
                case 0:
                case 3:
                case 9:
                    if (state == 3 || state == 9)
                    {
                        ResetVariables();
                    }
                    // the screen currently reads "0"
                    if (ScreenTb.Text == "0")
                    {
                        // display the new digit on the screen
                        ScreenTb.Text = number.ToString();
                    }
                    // the screen is displaying a number other than "0"
                    else
                    {
                        // display the currently displayed number and concatinate the new digit on the end
                        ScreenTb.Text = ScreenTb.Text + number.ToString();
                    }
                    break;
                case 1:
                    ScreenTb.Text = number.ToString();
                    state = 2;
                    break;
                case 2:
                    // the second number has already been partially entered and is not 0
                    if (ScreenTb.Text != "0")
                    {
                        // display the currently displayed number and concatinate the new digit on the end
                        ScreenTb.Text = ScreenTb.Text + number.ToString();
                    }
                    // no part of the second number has been entered yet
                    else
                    {
                        // display the new digit on the screen
                        ScreenTb.Text = number.ToString();
                    }
                    break;

            }
            // prevent a number longer than 25 characters being entered
            if (ScreenTb.Text.Length > 25)
                BackSpace();
        }

        // Number Buttons
        private void OneBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(1);
        }
        private void TwoBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(2);
        }
        private void ThreeBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(3);
        }
        private void FourBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(4);
        }
        private void FiveBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(5);
        }
        private void SixBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(6);
        }
        private void SevenBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(7);
        }
        private void EightBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(8);
        }
        private void NineBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(9);
        }
        private void ZeroBtn_Click(object sender, EventArgs e)
        {
            NumberButtonPressed(0);
        }

        // Operator Buttons
        private void PlusBtn_Click(object sender, EventArgs e)
        {
            Operation("+");
        }
        private void SubtractBtn_Click(object sender, EventArgs e)
        {
            Operation("-");
        }
        private void DivideBtn_Click(object sender, EventArgs e)
        {
            Operation("/");
        }
        private void MultiplyBtn_Click(object sender, EventArgs e)
        {
            Operation("*");
        }

        // Equals Button
        private void EqualsBtn_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                    // Do nothing
                    break;
                case 1:
                    // Do nothing
                    break;
                case 2:
                    if (ScreenTb.Text == "-")
                        Error();
                    else
                    {
                        secondNumber = Decimal.Parse(ScreenTb.Text);
                        switch (operation)
                        {
                            case "+":
                                AddTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "-":
                                SubtractTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "/":
                                DivideTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "*":
                                MultiplyTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                        }
                        if (state != 9)
                            state = 3;
                    }
                    break;
                case 3:
                    switch (operation)
                    {
                        case "+":
                            AddTwoNumbers(secondNumber, Decimal.Parse(ScreenTb.Text));
                            break;
                        case "-":
                            SubtractTwoNumbers(Decimal.Parse(ScreenTb.Text), secondNumber);
                            break;
                        case "/":
                            DivideTwoNumbers(Decimal.Parse(ScreenTb.Text), secondNumber);
                            break;
                        case "*":
                            MultiplyTwoNumbers(secondNumber, Decimal.Parse(ScreenTb.Text));
                            break;
                    }
                    break;
                case 9:
                    // Do nothing
                    break;
            }
        }

        // Operation Method
        private void Operation(string op)
        {
            switch (state)
            {
                case 0:
                    if (op == "-" && ScreenTb.Text == "0")
                        ScreenTb.Text = "-";
                    else if (ScreenTb.Text == "-")
                    {
                        // Do nothing
                    }
                    else
                    {
                        // save the number on screen as the first number
                        firstNumber = Decimal.Parse(ScreenTb.Text);
                        // set the current operator
                        operation = op;
                        // set the state to 1
                        state = 1;
                    }
                    break;
                case 1:
                    if (op == "-")
                    {
                        ScreenTb.Text = "-";
                        state = 2;
                    }
                    else
                        operation = op;
                    break;
                case 2:
                    // If minus is already displayed and is pressed again, do nothing
                    if (ScreenTb.Text == "-" && operation == "-")
                    {
                        // Do Nothing
                    }
                    else
                    {
                        // do calculation
                        switch (operation)
                        {
                            case "+":
                                AddTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "-":
                                SubtractTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "/":
                                DivideTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                            case "*":
                                MultiplyTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text));
                                break;
                        }
                        firstNumber = Decimal.Parse(ScreenTb.Text);
                        operation = op;
                        if (state != 9)
                            state = 1;
                    }
                    break;
                case 3:
                    firstNumber = Decimal.Parse(ScreenTb.Text);
                    operation = op;
                    state = 1;
                    break;
            }
        }

        // Operation Methods
        private void AddTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            try
            {
                ScreenTb.Text = (firstNumber + secondNumber).ToString();
                TrimNumber();
            }
            catch
            {
                Error();
            }
        }
        private void SubtractTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            try
            {
                ScreenTb.Text = (firstNumber - secondNumber).ToString();
                TrimNumber();
            }
            catch
            {
                Error();
            }
        }
        private void DivideTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            // prevent divide by zero exceptions
            if (firstNumber == 0 || secondNumber == 0)
                // Avoid divide by zero exeption, display "0" and set state back to 0
                ResetVariables();
            else
            {
                try
                {
                    ScreenTb.Text = (firstNumber / secondNumber).ToString();
                    TrimNumber();
                }
                catch
                {
                    Error();
                }
            }  
        }
        private void MultiplyTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            try
            {
                ScreenTb.Text = (firstNumber * secondNumber).ToString();
                TrimNumber();
            }
            catch
            {
                Error();
            }
        }
       
        // Clear Buttons
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResetVariables();
        }
        private void ClearEntryBtn_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                    ScreenTb.Text = "0";
                    break;
                case 1:
                    ScreenTb.Text = "0";
                    state = 0;
                    break;
                case 2:
                    ScreenTb.Text = "0";
                    break;
                case 3:
                case 9:
                    ResetVariables();
                    break;
            }
            
        }

        // Memory Methods
        private void MemoryAddBtn_Click(object sender, EventArgs e)
        {
            if (state != 9 && ScreenTb.Text != "0" && ScreenTb.Text != "-")
                memoryNumber += Decimal.Parse(ScreenTb.Text);
        }
        private void MemoryReturnBtn_Click(object sender, EventArgs e)
        {
            if (memoryNumber != 0)
            {
                switch (state)
                {
                    case 0:
                    case 2:
                        ScreenTb.Text = memoryNumber.ToString();
                        break;
                    case 1:
                        ScreenTb.Text = memoryNumber.ToString();
                        state = 2;
                        break;
                    case 3:
                    case 9:
                        ResetVariables();
                        ScreenTb.Text = memoryNumber.ToString();
                        break;
                }
            }
        }
        private void MemoryClearBtn_Click(object sender, EventArgs e)
        {
            memoryNumber = 0;
        }
        private void MemoryMinus_Click(object sender, EventArgs e)
        {
            if (state != 9 && ScreenTb.Text != "0" && ScreenTb.Text != "-")
                memoryNumber -= Decimal.Parse(ScreenTb.Text);
        }

        // Decimal Point Button
        private void DecimalBtn_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                case 2:
                    // Make sure that the current number doesn't already contain a decimal point and that its not too long to add too
                    if (!ScreenTb.Text.Contains('.')  && ScreenTb.Text.Length <= 25)
                        // concatinate a decimal point to the end of the currently displayed number
                        ScreenTb.Text += ".";
                    break;
                case 1:
                    // second number has not been started so set screen to display "."
                    ScreenTb.Text = ".";
                    // set the new state
                    state = 2;
                    break;
                case 3:
                    // pressing decimal after the equals button clears the last operation and displays a decimal point
                    ResetVariables();
                    ScreenTb.Text = ".";
                    break;
                case 9:
                    // Do nothing
                    break;
            }
        }

        // Delete / Backspace Button
        private void BackspaceBtn_Click(object sender, EventArgs e)
        {
            BackSpace();
        }
        private void BackSpace()
        {
            switch (state)
            {
                case 0:
                case 2:
                    // remove the last number/character in the display
                    ScreenTb.Text = ScreenTb.Text.Remove(ScreenTb.Text.Length - 1);
                    // afterwards if no number exists, display "0"
                    if (ScreenTb.Text == "")
                        ScreenTb.Text = "0";
                    break;
                case 1:
                    // remove the last number/character in the display
                    ScreenTb.Text = ScreenTb.Text.Remove(ScreenTb.Text.Length - 1);
                    // afterwards if no number exists, display "0"
                    if (ScreenTb.Text == "")
                        ScreenTb.Text = "0";
                    // update the first number variable
                    firstNumber = Decimal.Parse(ScreenTb.Text);
                    break;
                case 3:
                case 9:
                    // Do Nothing
                    break;
            }
        }

        // Percentage Button
        private void PercentBtn_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case 0:
                    // if display doesn't read "0", divide the displayed number by 100
                    if (ScreenTb.Text != "0" && ScreenTb.Text != "-")
                        ScreenTb.Text = (Decimal.Parse(ScreenTb.Text) / 100).ToString();
                    break;
                case 2:
                    if (ScreenTb.Text != "-")
                    {
                        switch (operation)
                        {
                            case "/":
                                // divide first number by number on screen, then times by 100
                                ScreenTb.Text = ((firstNumber / Decimal.Parse(ScreenTb.Text)) * 100).ToString();
                                break;
                            case "+":
                                // divide number on screen by 100, then add first number
                                ScreenTb.Text = ((Decimal.Parse(ScreenTb.Text) / 100) + firstNumber).ToString();
                                break;
                            case "-":
                                // divide number on screen by 100, then subtract it from first number
                                ScreenTb.Text = (firstNumber - (Decimal.Parse(ScreenTb.Text) / 100)).ToString();
                                break;
                            case "*":
                                // divide number on screen by 100, then multiply it by first number
                                ScreenTb.Text = ((Decimal.Parse(ScreenTb.Text) / 100) * firstNumber).ToString();
                                break;
                        }
                        state = 0;
                    }
                    break;
                
                case 3:
                    // if display doesn't read "0", divide the displayed number by 100
                    if (ScreenTb.Text != "0")
                        ScreenTb.Text = (Decimal.Parse(ScreenTb.Text) / 100).ToString();
                    break;
                case 1:
                case 9:
                    // Do nothing
                    break;
            }
        }

        // Trim Method
        private void TrimNumber()
        {
            // The screen is only large enough to display 26 characters in total.
            // If the number contains a decimal point and its more than 24 places back, it means the whole part of the number
            // is too large to fit on the screen and an error should be displayed

            // if the number is smaller than 25 characters, do nothing
            if (ScreenTb.Text.Length > 25)
            {
                // the number is more than 25 characters and contains a decimal point
                if (ScreenTb.Text.Contains('.'))
                {
                    // remove all characters after the 25th character
                    ScreenTb.Text.Remove(ScreenTb.Text.Length - (ScreenTb.Text.Length - 25));
                    // the number no longer contains a decimal point therefore is too large for the whole number to be displayed
                    if (!ScreenTb.Text.Contains('.'))
                    {
                        // display an "e" at the end to signify an error 
                        ScreenTb.Text += "e";
                        // set the error state
                        state = 9;
                    }
                }
                // the number is too large to fit in the screen, display an error
                else
                {
                    // reduce the number down to its first 25 characters
                    ScreenTb.Text.Remove(ScreenTb.Text.Length - (ScreenTb.Text.Length - 25));
                    // display an "e" at the end to signify an error 
                    ScreenTb.Text += "e";
                    // set the error state
                    state = 9;
                }
            }
        }

        // Error Method
        private void Error()
        {
            ScreenTb.Text = "e";
            state = 9;
        }

        // Reset Variables Method
        private void ResetVariables()
        {
            // reset/clear variables
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            ScreenTb.Text = "0";
            state = 0;
        }
    }
}

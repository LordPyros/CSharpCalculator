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
        // Flag used to indicate whether an operater button has been pressed
        bool operatorUsed;
        // Flag to indicate whether the second number of the operation has began to be entered yet
        bool secondNumberStarted;
        // Flag used to indicate when a number cannot be edited. eg after pressing enquals for an operation, the result cannot be appended too or backspaced, It must be cleared
        bool unmodifiableNumber;
        // Flag to indicate when an operator button was the last button to be pressed,
        // Used to prevent unexpected results from pressing operators multiple times.
        bool operatorPressedLast;
        // Flag to indicate when the current result is an error
        bool error;

        public Form1()
        {
            InitializeComponent();

            // Set the intial display as "0"
            ScreenTb.Text = "0";
        }

        // Number Method
        private void NumberButtonPressed(int number)
        {
            // Clear everything if equals was pressed last or if theres an error and then a number is entered.
            if (unmodifiableNumber || error)
            {
                ResetVariables();
            }
            // Reset operatorPressedLast flag
            operatorPressedLast = false;

            // No current operator, so the first number either has not started to be entered yet or is still being entered
            if (!operatorUsed)
            {
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
            }
            // There is an operator so a first number already exists
            else
            {
                // the second number has already been partially entered and is not 0
                if (secondNumberStarted && ScreenTb.Text != "0")
                {
                    // display the currently displayed number and concatinate the new digit on the end
                    ScreenTb.Text = ScreenTb.Text + number.ToString();
                }
                // no part of the second number has been entered yet
                else
                {
                    // display the new digit on the screen
                    ScreenTb.Text = number.ToString();
                    // the second number has now been started so set the secondNumberStarted flag to true
                    secondNumberStarted = true;
                }
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
            if (!error)
            {
                // Reset operatorPressedLast flag
                operatorPressedLast = false;

                // do nothing if an operator has not been used
                if (operatorUsed)
                {
                    // set the currently displayed number as the second number variable if its the first time the equals button was pressed (if the equals
                    // button is pressed several times in a row, this will only happen on the first press)
                    if (!unmodifiableNumber)
                        secondNumber = Decimal.Parse(ScreenTb.Text);

                    // do calculation
                    switch (operation)
                    {
                        case "+":
                            Addition();
                            break;
                        case "-":
                            try
                            {
                                ScreenTb.Text = SubtractTwoNumbers(firstNumber, secondNumber).ToString();
                                TrimNumber();
                            }
                            catch
                            {
                                Error();
                            }
                            break;
                        case "/":
                            try
                            {

                                ScreenTb.Text = DivideTwoNumbers(firstNumber, secondNumber).ToString();
                                TrimNumber();
                            }
                            catch
                            {
                                Error();
                            }
                            break;
                        case "*":
                            Multiplication();
                            break;
                    }

                    // For Subtraction and division, save the currently displayed number to the first number variable
                    if (operation == "-" || operation == "/")
                    {
                        firstNumber = Decimal.Parse(ScreenTb.Text);
                    }
                    // For addition and multiplication, save the second number as the first number only the first time equals is pressed.
                    else if (!unmodifiableNumber)
                    {
                        if (operation == "+" || operation == "*")
                        {
                            firstNumber = secondNumber;
                        }
                    }

                    // set number as unmodifiable to prevent a digit being concatinated on the end if the user presses a number button.
                    // eg. after pressing equals then a number, the old number should be deleted and be replaced by a new digit.
                    unmodifiableNumber = true;
                }
            }
        }

        // Operation Method
        private void Operation(string op)
        {
            if (!error)
            {
                // do nothing except update the operator if 2 operators button have been pressed in a row
                if (!operatorPressedLast)
                {
                    // equals button wasn't the last button pressed
                    if (!unmodifiableNumber)
                    {
                        // check if there is a current operator
                        if (operatorUsed)
                        {
                            // do calculation
                            switch (op)
                            {
                                case "+":
                                    Addition();
                                    break;
                                case "-":
                                    try
                                    {
                                        ScreenTb.Text = SubtractTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                                        TrimNumber();
                                    }
                                    catch
                                    {
                                        Error();
                                    }
                                    break;
                                case "/":
                                    try
                                    {
                                        ScreenTb.Text = DivideTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                                        TrimNumber();
                                    }
                                    catch
                                    {
                                        Error();
                                    }
                                    break;
                                case "*":
                                    Multiplication();
                                    break;
                            }
                            // operation has just been done so set the secondNumberStarted flag to false
                            secondNumberStarted = false;
                        }
                    }
                    // equals button was the last button pressed
                    else
                    {
                        // the number is unmodifiable so the second number cannot have been started - set secondNumberStarted variable to false
                        secondNumberStarted = false;
                        // allow the number on the current display to be modified
                        unmodifiableNumber = false;

                    }
                    // set the number on the display to the first number variable
                    firstNumber = Decimal.Parse(ScreenTb.Text);
                    // set the operatorUsed flag to true
                    operatorUsed = true;
                }
                // set the current operation
                operation = op;
                // set the operatorPressedLast flag to true
                operatorPressedLast = true;
            }
        }

        // Operation Methods
        private decimal AddTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber + secondNumber;
        }
        private decimal SubtractTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber - secondNumber;
        }
        private decimal DivideTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            // prevent divide by zero exceptions
            if (firstNumber == 0 || secondNumber == 0)
                return 0;
            // divide numbers
            return firstNumber / secondNumber;
        }
        private decimal MultiplyTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber;
        }
        private void Addition()
        {
            try
            {
                ScreenTb.Text = AddTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                TrimNumber();
            }
            catch
            {
                Error();
            }
        }
        private void Multiplication()
        {
            try
            {
                ScreenTb.Text = MultiplyTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                TrimNumber();
            }
            catch
            {
                Error();
            }
        }
        private decimal GetPercentageOfTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            // Normal percentage calculations
            // Check that an operator has been used and neither number is "0"
            if (operatorUsed)
            {
                if (firstNumber != 0 && ScreenTb.Text != "0")
                {
                    switch (operation)
                    {
                        case "/":
                            unmodifiableNumber = true;
                            // divide first number by number on screen, then times by 100
                            return (firstNumber / Decimal.Parse(ScreenTb.Text)) * 100;
                        case "+":
                            unmodifiableNumber = true;
                            // divide number on screen by 100, then add first number
                            return (Decimal.Parse(ScreenTb.Text) / 100) + firstNumber;
                        case "-":
                            unmodifiableNumber = true;
                            // divide number on screen by 100, then subtract it from first number
                            return firstNumber - (Decimal.Parse(ScreenTb.Text) / 100);
                        case "*":
                            unmodifiableNumber = true;
                            // divide number on screen by 100, then multiply it by first number
                            return (Decimal.Parse(ScreenTb.Text) / 100) * firstNumber;
                    }
                }
            }

            // percentage calculation on only one number
            // check that no operator has been used and the screen is not displaying "0"
            if (!operatorUsed && ScreenTb.Text != "0")
            {
                unmodifiableNumber = true;
                // Divide the number on screen by 100
                return Decimal.Parse(ScreenTb.Text) / 100;
            }

            // *** why do i reset varibles here??
            ResetVariables();

            // All other situation should result in a zero
            return 0;
        }

        // Clear Buttons
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResetVariables();
        }
        private void ClearEntryBtn_Click(object sender, EventArgs e)
        {
            ScreenTb.Text = "0";
            unmodifiableNumber = false;
            error = false;
        }

        // Memory Methods
        private void MemoryAddBtn_Click(object sender, EventArgs e)
        {
            if (!error)
                memoryNumber += Decimal.Parse(ScreenTb.Text);
        }
        private void MemoryReturnBtn_Click(object sender, EventArgs e)
        {
            if (memoryNumber != 0)
                ScreenTb.Text = memoryNumber.ToString();
        }
        private void MemoryClearBtn_Click(object sender, EventArgs e)
        {
            memoryNumber = 0;
        }
        private void MemoryMinus_Click(object sender, EventArgs e)
        {
            if (!error)
                memoryNumber -= Decimal.Parse(ScreenTb.Text);
        }

        // Decimal Point Button
        private void DecimalBtn_Click(object sender, EventArgs e)
        {
            // check that the currently displayed number does not already contain a decimal point
            if (!ScreenTb.Text.Contains('.') && !unmodifiableNumber && !error)
            {
                // concatinate a decimal point to the end of the currently displayed number
                ScreenTb.Text += ".";
                // If the decimal point is accepted, set the operatorPressedLast flag to false
                operatorPressedLast = false;
            }
        }

        // Delete / Backspace Button
        private void BackspaceBtn_Click(object sender, EventArgs e)
        {
            BackSpace();
        }
        private void BackSpace()
        {
            // check the if current displays is allowed to be edited
            if (!unmodifiableNumber && !error)
            {
                // remove the last number/character in the display
                ScreenTb.Text = ScreenTb.Text.Remove(ScreenTb.Text.Length - 1);
                // afterwards if no number exists, display "0"
                if (ScreenTb.Text == "")
                    ScreenTb.Text = "0";
            }
        }

        // Percentage Button
        private void PercentBtn_Click(object sender, EventArgs e)
        {
            if (!unmodifiableNumber && !error)
            {
                ScreenTb.Text = GetPercentageOfTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                TrimNumber();
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
                        // set the error flag to true
                        error = true;
                    }
                }
                // the number is too large to fit in the screen, display an error
                else
                {
                    // reduce the number down to its first 25 characters
                    ScreenTb.Text.Remove(ScreenTb.Text.Length - (ScreenTb.Text.Length - 25));
                    // display an "e" at the end to signify an error 
                    ScreenTb.Text += "e";
                    // set the error flag to true
                    error = true;
                }
            }
        }
                
        // Error Method
        private void Error()
        {
            ScreenTb.Text = "e";
            error = true;
        }

        // Reset Variables Method
        private void ResetVariables()
        {
            // reset/clear variables
            operatorUsed = false;
            operatorPressedLast = false;
            secondNumberStarted = false;
            unmodifiableNumber = false;
            error = false;
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            ScreenTb.Text = "0";
        }
    }
}

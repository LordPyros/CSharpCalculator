using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpCalculator
{
    public partial class Form1 : Form
    {
        // memory remind needs fixing? test (probably test whole memory system)

        // need a memory minus button and change button text from minus to clear
        
        // need to add percentage


        decimal firstNumber = 0;
        decimal secondNumber = 0;
        decimal memoryNumber = 0;
        string operation = "";
        bool operatorUsed;
        bool secondNumberStarted;
        bool unmodifiableNumber;
        bool operatorPressedLast;

        public Form1()
        {
            InitializeComponent();

            ScreenTb.Text = "0";
        }

        private void NumberButtonPressed(int number)
        {
            // Clear everything if equals was pressed last and then a number is entered.
            if (unmodifiableNumber)
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
        }

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

        private void EqualsBtn_Click(object sender, EventArgs e)
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
                        ScreenTb.Text = AddTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                        break;
                    case "-":
                        ScreenTb.Text = SubtractTwoNumbers(firstNumber, secondNumber).ToString();
                        break;
                    case "/":
                        ScreenTb.Text = DivideTwoNumbers(firstNumber, secondNumber).ToString();
                        break;
                    case "*":
                        ScreenTb.Text = MultiplyTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
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
            return firstNumber / secondNumber;
        }
        private decimal MultiplyTwoNumbers(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber;
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResetVariables();
        }
        private void ClearEntryBtn_Click(object sender, EventArgs e)
        {
            ScreenTb.Text = "0";
            unmodifiableNumber = false;
        }

        private void ResetVariables()
        {
            // reset/clear variables
            operatorUsed = false;
            operatorPressedLast = false;
            secondNumberStarted = false;
            unmodifiableNumber = false;
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            ScreenTb.Text = "0";
        }

        private void MemoryAddBtn_Click(object sender, EventArgs e)
        {
            memoryNumber = Decimal.Parse(ScreenTb.Text);
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

        private void Operation(string op)
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
                                ScreenTb.Text = AddTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                                break;
                            case "-":
                                ScreenTb.Text = SubtractTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                                break;
                            case "/":
                                ScreenTb.Text = DivideTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
                                break;
                            case "*":
                                ScreenTb.Text = MultiplyTwoNumbers(firstNumber, Decimal.Parse(ScreenTb.Text)).ToString();
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

        private void DecimalBtn_Click(object sender, EventArgs e)
        {
            // check that the currently displayed number does not already contain a decimal point
            if (!ScreenTb.Text.Contains('.') && !unmodifiableNumber)
            {
                // concatinate a decimal point to the end of the currently displayed number
                ScreenTb.Text += ".";
                // If the decimal point is accepted, set the operatorPressedLast flag to false
                operatorPressedLast = false;
            }
        }

        private void BackspaceBtn_Click(object sender, EventArgs e)
        {
            // check the if current displays is allowed to be edited
            if (!unmodifiableNumber)
            {
                // remove the last number/character in the display
                ScreenTb.Text = ScreenTb.Text.Remove(ScreenTb.Text.Length - 1);
                // afterwards if no number exists, display "0"
                if (ScreenTb.Text == "")
                    ScreenTb.Text = "0";
            }
        }
    }
}

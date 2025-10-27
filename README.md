# SimpleCalculator
A skill check project written in C#. A console-based calculator application.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Logic Breakdown / Tasks](#logic-breakdown--tasks)
- [Code Structure](#code-structure)

## Overview
This project demonstrates:
- Loops and conditionals
- Exception handling
- Encapsulation
- Validating and parsing user input
- Switch expressions and LINQ

## Features
- Splits user input by commas.
- Uses LINQ to separate numbers and mathematical operators into lists.
- Follows PEMDAS, handling multiplication and division before addition and subtraction.
- Displays the result of the user's equation.
- Prompts the user to continue or exit the application.

## Logic Breakdown / Tasks 
- [x] Split user input into strings.
- [x] Parse input into doubles and operators.
- [x] Follow order of operations in accordance with PEMDAS.
- [x] Validate incorrect input without stopping the application.

## Code Structure
- `Program.cs` — Entry point.

- `CalculatorLogic.cs` — Static class containing:
   - `PromptUser()` — Handles user input, exceptions, and program flow.
   - `SplitInput()` — Uses `Split()` to separate input into strings.
   - `ConvertOperator()` — Converts string operators into mathematical operators with validation.
   - `Calculate()` — Solves the equation according to PEMDAS.
   - `ContinueUsing()` — Asks the user whether they want to continue.
import tkinter as tk
from tkinter import messagebox

class TicTacToe:
    """
    This class represents a Tic Tac Toe game application using Tkinter.
    """

    def __init__(self, root):
        """
        Initialize the game board and GUI elements.
        
        :param root: A Tkinter root window.
        """
        self.root = root
        self.root.title('Tic Tac Toe')
        self.turn = 'X'  # Start the game with player 'X'
        self.board = [''] * 9  # Represents a 3x3 game board as a list

        # Create and place buttons on the grid
        self.buttons = [
            tk.Button(root, width=10, height=3, font=('Arial', 24), 
                      command=lambda i=i: self.click(i)) for i in range(9)
        ]
        for i, button in enumerate(self.buttons):
            row, col = divmod(i, 3)
            button.grid(row=row, column=col)

    def click(self, i):
        """
        Handles a click event on the game board.
        
        :param i: Index of the button in the board.
        """
        # If the button hasn't been clicked and no winner yet
        if not self.board[i] and not self.check_winner():
            self.board[i] = self.turn  # Set the turn value in the board
            self.buttons[i].config(text=self.turn, state='disabled')  # Update button text and disable it
            winner = self.check_winner()  # Check for a winner
            if winner:
                messagebox.showinfo('Game Over', f'{winner} has won the game!')
                self.reset_game()  # Reset the game if there's a winner
            else:
                # Swap the player turn
                self.turn = 'O' if self.turn == 'X' else 'X'

    def check_winner(self):
        """
        Checks the game board for a winner.
        
        :return: 'X' or 'O' if a winner is found, 'Tie' if the board is full without a winner, or '' otherwise.
        """
        # Win conditions based on the index of the game board
        win_conditions = [
            [0, 1, 2], [3, 4, 5], [6, 7, 8],  # Rows
            [0, 3, 6], [1, 4, 7], [2, 5, 8],  # Columns
            [0, 4, 8], [2, 4, 6]              # Diagonals
        ]
        
        for condition in win_conditions:
            if self.board[condition[0]] == self.board[condition[1]] == self.board[condition[2]] and self.board[condition[0]]:
                return self.board[condition[0]]  # Return the winner ('X' or 'O')
        
        if '' not in self.board:
            messagebox.showinfo('Game Over', 'The game is a tie!')
            self.reset_game()
            return 'Tie'  # Return 'Tie' if the board is full and no winner
        
        return ''  # Return an empty string if there's no winner or tie yet

    def reset_game(self):
        """
        Resets the game board for a new game.
        """
        self.board = [''] * 9
        self.turn = 'X'  # Reset turn to 'X'
        for button in self.buttons:
            button.config(text='', state='normal')  # Clear all buttons

# Main script
if __name__ == '__main__':
    root = tk.Tk()
    game = TicTacToe(root)  # Create a TicTacToe game instance
    root.mainloop()  # Start the Tkinter event loop

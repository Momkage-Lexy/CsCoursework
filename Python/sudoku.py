def create_sudoku_board(size=9):
    """
    Create a blank Sudoku board of the given size.
    """
    return [[0 for _ in range(size)] for _ in range(size)]

def print_board(board):
    """
    Print the Sudoku board in a readable format.
    """
    for row in board:
        print(" ".join(str(num) if num != 0 else '.' for num in row))

def is_valid(board, row, col, num):
    """
    Check if it's valid to place a number in a specific cell of the board.
    """
    # Check if the number is not in the given row
    for x in range(9):
        if board[row][x] == num:
            return False

    # Check if the number is not in the given column
    for x in range(9):
        if board[x][col] == num:
            return False

    # Check if the number is not in the given 3x3 square
    startRow = row - row % 3
    startCol = col - col % 3
    for i in range(3):
        for j in range(3):
            if board[i + startRow][j + startCol] == num:
                return False

    return True

def solve_sudoku(board):
    """
    Solve the Sudoku puzzle using backtracking algorithm.
    """
    empty_found = False
    for i in range(9):
        for j in range(9):
            if board[i][j] == 0:
                row, col = i, j
                empty_found = True
                break
        if empty_found:
            break

    if not empty_found:
        return True  # Puzzle solved

    for num in range(1, 10):
        if is_valid(board, row, col, num):
            board[row][col] = num
            if solve_sudoku(board):
                return True
            board[row][col] = 0  # Reset cell

    return False

# Create a new Sudoku board
sudoku_board = create_sudoku_board()

# Example: Pre-fill some cells with numbers (for demonstration purpose)
sudoku_board[0][0] = 5
sudoku_board[1][1] = 6
sudoku_board[2][2] = 7
sudoku_board[3][3] = 8
sudoku_board[4][4] = 9

# Print the initial board
print("Initial Board:")
print_board(sudoku_board)

# Solve the Sudoku puzzle
if solve_sudoku(sudoku_board):
    print("\nSolved Board:")
    print_board(sudoku_board)
else:
    print("\nNo solution exists")

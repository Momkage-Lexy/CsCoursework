public class SquareRobot : IRobot
{
    private int x = 0, y = 0;   // Starting position at (0, 0)
    private int dx = 0, dy = 1; // Initial direction facing north (up)

    private HashSet<(int, int)> visited = new HashSet<(int, int)>(); // Track visited coordinates

    public bool Move(int[] commands)
    {        
        // Validate input constraints
        if (!ValidateCommands(commands))
        {
            throw new ArgumentException("Invalid input: Ensure the length of the commands array is between 3 and 100 and all commands are between 1 and 10,000.");
        }
        visited.Add((x, y));  // Start position

        foreach (var command in commands)
        {
            // Move the robot in the current direction
            for (int i = 0; i < command; i++)
            {
                x += dx;
                y += dy;
                if (visited.Contains((x, y)))
                {
                    return true;  // The robot revisits a square
                }
                visited.Add((x, y));
            }

            // Turn 90 degrees clockwise: (dx, dy) -> (dy, -dx)
            int temp = dx;
            dx = dy;
            dy = -temp;
        }

        return false;  // No revisits found
    }
    private bool ValidateCommands(int[] commands)
    {
        // Check array length constraint
        if (commands.Length < 3 || commands.Length > 100)
        {
            return false;
        }

        // Check each command value constraint
        foreach (int command in commands)
        {
            if (command < 1 || command > 10000)
            {
                return false;
            }
        }

        return true;
    }
    public List<(int, int)> Locate(int[] commands)
    {
        visited.Clear();
        x = 0;
        y = 0;
        dx = 0;
        dy = 1;
        List<(int, int)> locations = new List<(int, int)>();

        foreach (var command in commands)
        {
            // Move and log position
            for (int i = 0; i < command; i++)
            {
                x += dx;
                y += dy;
                locations.Add((x, y));
            }

            // Turn 90 degrees clockwise
            int temp = dx;
            dx = dy;
            dy = -temp;
        }

        return locations;
    }
}
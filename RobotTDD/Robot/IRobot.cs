/** Robot interface. Defines the method(s) that
    *  a robot must implement.
    */
public interface IRobot
{
    /** Move the robot.
        *  @param commands The commands to move the robot.
        *  @return True if the robot successfully moved, false otherwise. Success is determined by the implementer.
        */
    bool Move(int[] commands);
}
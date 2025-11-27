public class WritingAssignment : Assignment
{
    private string _title;

    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        _title = title;
    }

    public string GetWritingInformation()
    {
        // Must use GetStudentName() because _studentName is private in base class
        return $"{_title} by {GetStudentName()}";
    }
}

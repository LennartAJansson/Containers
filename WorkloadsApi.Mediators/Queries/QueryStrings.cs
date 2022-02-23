namespace WorkloadsApi.Mediators.Queries
{
    public static class QueryStrings
    {
        public static readonly string SelectAllJoined =
            @$"SELECT w.WorkloadId, w.Start, w.Stop, w.AssignmentId, w.PersonId, a.AssignmentId, a.CustomerName, a.Description, p.PersonId, p.Name
FROM Workloads AS w
JOIN Assignments AS a ON w.AssignmentId = a.AssignmentId
JOIN People AS p ON w.PersonId = p.PersonId";
        public static readonly string SelectAllJoinedWhereAssignment =
            @$"SELECT w.WorkloadId, w.Start, w.Stop, w.AssignmentId, w.PersonId, a.AssignmentId, a.CustomerName, a.Description, p.PersonId, p.Name
FROM Workloads AS w
JOIN Assignments AS a ON w.AssignmentId = a.AssignmentId
JOIN People AS p ON w.PersonId = p.PersonId
WHERE w.AssignmentId = @p1";
        public static readonly string SelectAllJoinedWherePerson =
            @$"SELECT w.WorkloadId, w.Start, w.Stop, w.AssignmentId, w.PersonId, a.AssignmentId, a.CustomerName, a.Description, p.PersonId, p.Name
FROM Workloads AS w
JOIN Assignments AS a ON w.AssignmentId = a.AssignmentId
JOIN People AS p ON w.PersonId = p.PersonId
WHERE w.PersonId = @p1";
        public static readonly string SelectAllJoinedWhereWorkload =
            @$"SELECT w.WorkloadId, w.Start, w.Stop, w.AssignmentId, w.PersonId, a.AssignmentId, a.CustomerName, a.Description, p.PersonId, p.Name
FROM Workloads AS w
JOIN Assignments AS a ON w.AssignmentId = a.AssignmentId
JOIN People AS p ON w.PersonId = p.PersonId
WHERE w.WorkloadId = @p1";
    }
}
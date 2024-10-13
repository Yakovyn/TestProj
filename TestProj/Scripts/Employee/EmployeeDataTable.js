$(document).ready(function () {
    $('#employeeTable').DataTable({
        "searching": true, // Enable searching
        "paging": true, // Enable pagination
        "ordering": true, // Enable sorting
        "info": true // Show information
    });

    // Handle search form submission
    $('#searchForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission
        var searchString = $('#searchString').val();
        var table = $('#employeeTable').DataTable();
        table.search(searchString).draw(); // Use DataTable's search method
    });
});
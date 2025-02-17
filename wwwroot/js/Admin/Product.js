$(document).ready(function () {
    $('#CategoryId').change(function () {
        var categoryId = $(this).val();
        var subCategoryDropdown = $('#SubCategoryId');

        alert(categoryId);

        if (categoryId) {
            $.ajax({
                url: '/Api/GetAllSubCat',             // Replace with your controller name----
                type: 'GET',
                data: { CategoryId: categoryId },
                success: function (data) {
                    subCategoryDropdown.empty();
                    subCategoryDropdown.append('<option value="">Select Sub-Category</option>');
                    $.each(data, function (index, item) {
                        subCategoryDropdown.append('<option value="' + item.id + '">' + item.subCategoryName + '</option>');
                    });
                },
                error: function () {
                    alert('Error fetching sub-categories.');
                }
            });
        } else {
            subCategoryDropdown.empty();
            subCategoryDropdown.append('<option value="">Select Sub-Category</option>');
        }
    });
});
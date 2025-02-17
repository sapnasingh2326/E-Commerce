$(document).ready(function () {
    $('#CountryId').change(function () {
        var countryId = $(this).val();
        var stateDropdown = $('#StateId');

        alert(countryId);

        if (countryId) {
            $.ajax({
                url: '/Api/GetAllCities',  // Corrected the controller name to a more appropriate one
                type: 'GET',
                data: { CountryId: countryId },
                success: function (data) { 
                    stateDropdown.empty();
                    stateDropdown.append('<option value="">Select state</option>');
                    $.each(data, function (index, item) {
                        stateDropdown.append('<option value="' + item.id + '">' + item.stateName + '</option>');
                    });
                },
                error: function () {
                    alert('Error fetching states.');
                }
            });
        } else {
            stateDropdown.empty();
            stateDropdown.append('<option value="">Select state</option>');
        }
    });
});

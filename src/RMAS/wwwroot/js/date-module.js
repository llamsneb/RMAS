var DateModule = (function () {

    // Format date for SQL insert.
    var sqlDate = function (date) {
        var curr_year = date.getFullYear();
        var curr_month = date.getMonth() + 1;
        var curr_date = date.getDate();
        return (curr_year + "-" + curr_month + "-" + curr_date);
    } 

    var getRoomNumbers = function (roomType) {
        let jqxhr = $.ajax({
            type: "POST",
            url: "/Reserve/GetRoomNumbers",
            data: { 'roomType': roomType },
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (function (response) {
                let select = document.getElementById("RoomNumber");
                select.length = 0;               

                response.forEach(function (num) {
                    select.options.add(new Option(num));
                })
            })
        });
    }

    function addHiddenDate(dateString) {
        var itemCount = $("input[name='Dates']").length;
        var dat = $("<input type='hidden' asp-for='Dates' name='Dates' id='Dates[" + itemCount + "]' /> ").val(dateString);
        $("form#Reserve").append(dat);
    }

    $(document).ready(function () {
        let selectedDates = [];

        $("#datepicker").datepicker({
            minDate: 0,
            // Called on date select
            onSelect: function (dateText, obj) {
                let dt = sqlDate(new Date(dateText));
                if (selectedDates.includes(dt)) {
                    selectedDates = selectedDates.filter(date => date !== dt);
                    $('input[name="Dates"][value=' + dt +']').remove();
                    $("div#datepicker td.date-selected.ui-datepicker-current-day").removeClass("date-selected");
                } else {
                    addHiddenDate(dt)
                    selectedDates.push(dt);
                }
            },
            // Called for each date in calendar
            beforeShowDay: function (date) {
                for (let i = 0; i < selectedDates.length; i++) {
                    if (sqlDate(date) == selectedDates[i]) {
                        return [true, "date-selected"];
                    }
                }
                return [true,''];
            },
            // Called before calendar loads
            onUpdateDatepicker: function (obj) {
                $("div#datepicker td .ui-state-active").removeClass("ui-state-active");               
            }            
        });        
    });        

    // Event Handlers:
    $('#RoomType').change(
        function () {
            getRoomNumbers(this.value)
        }
    );

    // Expose Methods:
    return {
        sqlDate: sqlDate,
        getRoomNumbers: getRoomNumbers
    };    
})();
var DateModule = (function () {

    // Format date for SQL insert.
    var sqlDate = function (date) {
        var curr_year = date.getFullYear();
        var curr_month = date.getMonth() + 1;
        var curr_date = date.getDate();
        return (curr_year + "-" + curr_month + "-" + curr_date);
    }

    // Format date to be shown in textbox.
    var boxDate = function (date) {
        var curr_year = date.getFullYear();
        var curr_month = date.getMonth() + 1;
        var curr_date = date.getDate();
        return (curr_month + "/" + curr_date + "/" + curr_year);
    }

    var setDateChkboxVals = function (dateText) {
        var prevSun = new Date(dateText);
        var d = prevSun.getDay();
        prevSun.setDate(prevSun.getDate() - d);
        document.getElementById("date").value = boxDate(prevSun);

        document.getElementById("Sun").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Mon").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Tues").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Wed").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Thur").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Fri").value = sqlDate(prevSun);
        prevSun.setDate(prevSun.getDate() + 1);
        document.getElementById("Sat").value = sqlDate(prevSun);
    }

    var getRooms = function (roomType) {
        let jqxhr = $.ajax({
            type: "POST",
            url: "/Reserve/GetRooms",
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

    $(document).ready(function () {
        var defaultDay = new Date();
        defaultDay.setDate(defaultDay.getDate() - defaultDay.getDay());
        setDateChkboxVals(defaultDay.toLocaleDateString());

        $("#datepicker").datepicker({
            defaultDate: defaultDay,
            minDate: 0,
            onSelect: function (dateText) { setDateChkboxVals(dateText) }
        });
    });    

    // Event Handlers:
    $('#RoomType').change(
        function () {
            getRooms(this.value)
        }
    );

    // Expose Methods:
    return {
        sqlDate: sqlDate,
        boxDate: boxDate,
        setDateChkboxVals: setDateChkboxVals,
        getRooms: getRooms
    };    
})();
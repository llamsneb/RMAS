// Write your Javascript code.
var siteModule = (function () {
    var getRoomNumbers = function (roomType) {
        let jqxhr = $.ajax({
            type: "POST",
            url: "/Reserve/GetRoomNumbers",
            data: { 'roomType': roomType },
            dataType: "json",
            success: (function (response) {
                let select = document.getElementById("RoomNumber");
                select.length = 0;

                response.forEach(function (num) {
                    select.options.add(new Option(num));
                })
            }),
            complete: (function (response) {
                document.getElementById('RoomNumber').dispatchEvent(new Event('change'));
            })
        });
    }

    // Event Handlers:
    $('#RoomType').on("change",
        function () {
            siteModule.getRoomNumbers(this.value);
        }
    );

    // Expose Methods:
    return {
        getRoomNumbers: getRoomNumbers
    };
})();
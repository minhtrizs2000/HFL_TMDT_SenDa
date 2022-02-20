﻿$(document).ready(function () {

    // Save all selects' id in an array
    // to determine which select's option and value would be changed
    // after you select an option in another select.
    var selectors = ['QuanHuyen', 'PhuongXa']

    $('select').on('change', function () {
        var index = selectors.indexOf(this.id)
        var value = this.value

        // check if is the last one or not
        if (index < selectors.length - 1) {
            var next = $('#' + selectors[index + 1])

            // Show all the options in next select
            $(next).find('option').show()
            if (value != "") {
                // if this select's value is not empty
                // hide some of the options
                $(next).find('option[data-value!=' + value + ']').hide()
            }

            // set next select's value to be the first option's value
            // and trigger change()
            $(next).val($(next).find("option:first").val()).change()
        }
    })
});

//$(document).ready(function () {

//    // Save all selects' id in an array 
//    // to determine which select's option and value would be changed
//    // after you select an option in another select.
//    var selectors = ['selectLevel', 'selectSubject', 'selectTopic']

//    $('select').on('change', function () {
//        var index = selectors.indexOf(this.id)
//        var value = this.value

//        // check if is the last one or not
//        if (index < selectors.length - 1) {
//            var next = $('#' + selectors[index + 1])

//            // Show all the options in next select
//            $(next).find('option').show()
//            if (value != "") {
//                // if this select's value is not empty
//                // hide some of the options 
//                $(next).find('option[data-value!=' + value + ']').hide()
//            }

//            // set next select's value to be the first option's value 
//            // and trigger change()
//            $(next).val($(next).find("option:first").val()).change()
//        }
//    })
//});

        

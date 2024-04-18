
$(function () {
    $("#TypeCode").change(function () {
        $.get("/category/getlistbytype", { typeCode: $(this).val() })
            .done(function (data) {
                //console.log(data);
                $('#ParentID').find("option").each(function (i) {
                    if (i != 0) $(this).remove();
                });
                //$('#ParentID').append($('<option>', {
                //    value: 0,
                //    text: 'No parent'
                //}));           
                addOption(data, 0, '');
                if ($("#hfParentID").val() != '')
                    $('#ParentID').val($("#hfParentID").val());
            });
    });
    addOption = function (data, pid, prefix) {
        var child = $.grep(data, function (n, i) {
            return n.parentID == pid;
        });

        if (child.length > 0)
            for (var i = 0; i < child.length; i++) {
                $('#ParentID').append($('<option>', {
                    value: child[i].id,
                    text: prefix + child[i].name
                }));
                addOption(data, child[i].id, prefix + '---');
            }
    }
    $("#TypeCode").trigger('change');
});
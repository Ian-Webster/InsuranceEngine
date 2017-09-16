/* question visibility */
var questionGroupPrefix = "#QuestionContainer_";

/* handles change of drop down list visibility */
function dropDownChanged(ddl) {

    //get the selected item in the dropdown list
    var element = $(ddl).find('option:selected');

    //get the dependant items attribute from the dropdown list
    var dependantItemsAttribute = $(ddl).attr('data-condisplay-dependantquestions');
    //split the dependant items into an array
    var dependantItems = dependantItemsAttribute.split("|");
    //reset the visibility of the dependant items
    setVisibility(dependantItems);

    //get the target items attribute for the possible answer
    var targetItemsAttribute = element.attr('data-condisplay-targetquestions');
    if (targetItemsAttribute)
    {
        //target items attribute is not empty
        //split the target items out into an array
        var targetItems = targetItemsAttribute.split("|");
        //set the visibility
        setVisibility(targetItems);
    }

}

/* handles change of radio button list visibility */
function radioButtonChanged(rb) {

    var dependantItemsAttribute = $(rb).attr('data-condisplay-dependantquestions');
    //split the dependant items into an array
    var dependantItems = dependantItemsAttribute.split("|");
    //reset the visibility of the dependant items
    setVisibility(dependantItems);

    //get the target items attribute for the possible answer
    var targetItemsAttribute = $(rb).attr('data-condisplay-targetquestions');

    if (targetItemsAttribute) {
        //target items attribute is not empty
        //split the target items out into an array
        var targetItems = targetItemsAttribute.split("|");
        //set the visibility
        setVisibility(targetItems);
    }
}

/* core code to set a question to be visible or hidden */
function setVisibility(items) {
    $(items).each(function (index) {
        var question = items[index].split(":")[0];
        var visible = items[index].split(":")[1] == "show";
        if (visible) {
            $(questionGroupPrefix + question).show();
        } else {
            $(questionGroupPrefix + question).hide();
        }
    });
}

/* question visibility end */
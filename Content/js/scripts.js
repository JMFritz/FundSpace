$(function(){
  console.log("start");
  // $("#toggle").click(function(event){
  //   event.preventDefault();
    var theToggle = document.getElementById('toggle');


    // hasClass
    function hasClass(elem, className) {
    	return new RegExp(' ' + className + ' ').test(' ' + elem.className + ' ');
    }
    // addClass
    function addClass(elem, className) {
        if (!hasClass(elem, className)) {
        	elem.className += ' ' + className;
        }
    }
    // removeClass
    function removeClass(elem, className) {
    	var newClass = ' ' + elem.className.replace( /[\t\r\n]/g, ' ') + ' ';
    	if (hasClass(elem, className)) {
            while (newClass.indexOf(' ' + className + ' ') >= 0 ) {
                newClass = newClass.replace(' ' + className + ' ', ' ');
            }
            elem.className = newClass.replace(/^\s+|\s+$/g, '');
        }
    }
    // toggleClass
    function toggleClass(elem, className) {
    	var newClass = ' ' + elem.className.replace( /[\t\r\n]/g, " " ) + ' ';
        if (hasClass(elem, className)) {
            while (newClass.indexOf(" " + className + " ") >= 0 ) {
                newClass = newClass.replace( " " + className + " " , " " );
            }
            elem.className = newClass.replace(/^\s+|\s+$/g, '');
        } else {
            elem.className += ' ' + className;
        }

    }

    theToggle.onclick = function() {
       toggleClass(this, 'on');
       return false;
       console.log("inside .onclick");
    }
    console.log("end");
  // });



// -------------------------- search!!!!!!

$('.search a').click(function(e) {
  e.preventDefault();
  // $('#search-panel').css('visibility', 'visible');
  $('#search-panel').addClass('panel-visible');

});

$('.cancel').click(function() {
  // $('#search-panel').css('visibility', 'hidden');
  $('#search-panel').removeClass('panel-visible');

});
$('.go-button').click(function() {
  // $('#search-panel').css('visibility', 'hidden');
  $('#search-panel').removeClass('panel-visible');

});

});

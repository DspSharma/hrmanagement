

function addUpdateUser() {

    let firstName = $('#firstName').val();
    let lastName = $('#lastName').val();
    let email = $('#email').val();
    let mobile = $('#mobile').val();
    let password = $('#password').val();
    let role = $('#role').val();

   
    var Nameregex = /\b.*[a-zA-Z ].\b/;
    var Emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var numbersregex = /^[0-9]+$/;

    $(".error").remove();

    if (firstName == '') {
        toastr.error("FirstName field is required");
        return false;
    }
    else if (firstName.length < 3 || firstName.length > 50) {
        toastr.error("firstName length should be between minlength 3 and maxlength 50");
        return false;
    }
    else if (!firstName.match(Nameregex)) {
        toastr.error("firstName should contain only alphabetic characters");
        return false;
    }
    else if (lastName == '') {
        toastr.error("lastName field is required");
        return false;
    }
    else if (lastName.length < 3 || lastName.length > 50) {
        toastr.error("lastName length should be between minlength 3 and maxlength 50");
        return false;
    }
    else if (!lastName.match(Nameregex)) {
        toastr.error("lastName should contain only alphabetic characters");
        return false;
    }
    else if (email == '') {
        toastr.error("email field is required");
        return false;
    }
    else if (!email.match(Emailregex)) {
        toastr.error("Invalid Email ");
        return false;
    }
    else if (mobile == '') {
        toastr.error("Mobile field is required");
        return false;
    }
    else if (mobile.length < 10 || mobile.length > 10) {
        toastr.error("Mobile Should be 10 dights ");
        return false;
    }
    else if (!mobile.match(numbersregex)) {
        toastr.error("Invalid Mobile Numbers");
        return false;
    }
    else if (password == '') {
        toastr.error("password field is required");
        return false;
    }
    else if (password.length < 6 || password.length > 30) {
        toastr.error("password length should be between minlength 3 and maxlength 30");
        return false;
    }
    else if (role == '') {
        toastr.error("Role field is required");
        return false;
    }
}

function addUpdateHoliday() {
    let holidayFromDate = $('#holidayFromDate').val();
    let holidayToDate = $('#holidayToDate').val();
    let title = $('#title').val();

    var Addressegex = /^[a-zA-Z][a-zA-Z0-9\s.,#-]*$/;
  

    $(".error").remove();

    if (holidayFromDate == '') {
        toastr.error("holiday From Date field is required");
        return false;
    }
    else if (holidayToDate == '') {
        toastr.error("holiday To Date field is required");
        return false;
    }
    else if (holidayToDate < holidayFromDate) {
        toastr.error("Holiday From Date should be less than Holiday To Date");
        return false;
    }
    else if (title == '') {
        toastr.error("Title field is required");
        return false;
    }
    else if (title.length < 3 || title.length > 50) {
        toastr.error("Title length should be between minlength 3 and maxlength 30");
        return false;
    }
    else if (!title.match(Addressegex)) {
        toastr.error("Title must start with alphabetic characters");
        return false;
    }
}

function addUpdateLeaveRequest() {
    let title = $('#title').val();
    let message = $('#message').val();
    let fromDate = $('#fromDate').val();
    let toDate = $('#toDate').val();

    var Addressegex = /^[a-zA-Z][a-zA-Z0-9\s.,#-]*$/;
    $(".error").remove();

    if (title == '') {
        toastr.error("Title field is required");
        return false;
    }
    else if (title.length < 3 || title.length > 50) {
        toastr.error("Title length should be between minlength 3 and maxlength 30");
        return false;
    }
    //else if (!title.match(Addressegex)) {
    //    toastr.error("Title must start with alphabetic characters");
    //    return false;
    //}
    else if (message == '') {
        toastr.error("Message field is required");
        return false;
    }
    else if (message.length < 3 || message.length > 500) {
        toastr.error("Message length should be between minlength 3 and maxlength 500");
        return false;
    }
    //else if (!message.match(Addressegex)) {
    //    toastr.error("Message must start with alphabetic characters");
    //    return false;
    //}
    else if (fromDate == '') {
        toastr.error("From Date field is required");
        return false;
    }
    else if (toDate == '') {
        toastr.error("To Date field is required");
        return false;
    }
    else if (toDate < fromDate) {
        toastr.error("From Date should be less than  To Date");
        return false;
    }
}

function addupdateUserProfile() {
    let firstName1 = $('#firstName1').val();
    let lastName1 = $('#lastName1').val();
    let email1 = $('#email1').val();
    let mobile1 = $('#mobile1').val();

    var Nameregex = /\b.*[a-zA-Z ].\b/;
    var Emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var numbersregex = /^[0-9]+$/;
    $(".error").remove();

    if (firstName1 == '') {
        toastr.error("firstName field is required");
        return false;
    }
    else if (firstName1.length < 2 || firstName1.length > 30) {
        toastr.error("FirstName length should be between minlength 2 and maxlength 30");
        return false;
    }
    else if (!firstName1.match(Nameregex)) {
        toastr.error("firstName should contain only alphabetic characters");
        return false;
    }
    else if (lastName1 == '') {
        toastr.error("lastName field is required");
        return false;
    }
    else if (lastName1.length < 2 || lastName1.length > 30) {
        toastr.error("lastName length should be between minlength 2 and maxlength 30");
        return false;
    }
    else if (!lastName1.match(Nameregex)) {
        toastr.error("lastName should contain only alphabetic characters");
        return false;
    }
    else if (email1 == '') {
        toastr.error("email field is required");
        return false;
    }
    else if (!email1.match(Emailregex)) {
        toastr.error("Invalid Email ");
        return false;
    }
    else if (mobile1 == '') {
        toastr.error("Mobile field is required");
        return false;
    }
    else if (mobile1.length < 10 || mobile1.length > 10) {
        toastr.error("Mobile Should be 10 dights ");
        return false;
    }
    else if (!mobile1.match(numbersregex)) {
        toastr.error("Invalid Mobile Numbers");
        return false;
    }
}

function addUpdateTimeSheet() {
    let projectName = $('#projectId').val();
    let taskTitle = $('#taskTitle').val();
    let taskDescription = $('#taskDescription').val();
    let hours = $('#hours').val();
    let status = $('#status').val();

    var Addressegex = /^[a-zA-Z][a-zA-Z0-9\s.,#-]*$/;
   
    $(".error").remove();

    if (projectName == '') {
        toastr.error("projectName field is required");
        return false;
    }
    else if (taskTitle == '') {
        toastr.error("Title field is required");
        return false;
    }
    else if (taskTitle.length < 3 || taskTitle.length > 50) {
        toastr.error("Title length should be between minlength 3 and maxlength 50");
        return false;
    }
    else if (!taskTitle.match(Addressegex)) {
        toastr.error("Title must start with alphabetic characters");
        return false;
    }
    else if (taskDescription == '') {
        toastr.error("Description field is required");
        return false;
    }
    else if (taskDescription.length < 3 || taskDescription.length > 300) {
        toastr.error("Description length should be between minlength 3 and maxlength 300");
        return false;
    }
    else if (!taskDescription.match(Addressegex)) {
        toastr.error("Description must start with alphabetic characters");
        return false;
    }
    else if (hours == '') {
        toastr.error("Hours field is required");
        return false;
    }
    else if (status == '') {
        toastr.error("status field is required");
        return false;
    }
}

function addUpdateApiCredential() {
    let projectName = $('#projectName').val();
    let description = $('#description').val();
    let apiKey = $('#apiKey').val();
    let clientId = $('#clientId').val();
    let clientSecret = $('#clientSecret').val();
    let allowLimit = $('#allowLimit').val();
    let service = $('#service').val();
    let apiHost = $('#apiHost').val();
    let consumedLimit = $('#consumedLimit').val();
    let status = $('#status').val();
    let password = $('#password').val();


    var Addressegex = /^[a-zA-Z][a-zA-Z0-9\s.,#-]*$/;
    var numbersregex = /^[0-9]+$/;

    $(".error").remove();

    if (projectName == '') {
        toastr.error("projectName field is required");
        return false;
    }
    else if (description == '') {
        toastr.error("Description field is required");
        return false;
    }
    else if (description.length < 3 || description.length > 300) {
        toastr.error("Description length should be between minlength 3 and maxlength 300");
        return false;
    }
    else if (!description.match(Addressegex)) {
        toastr.error("Description must start with alphabetic characters");
        return false;
    }
    else if (apiKey == '') {
        toastr.error("apiKey field is required");
        return false;
    }
    else if (apiKey.length < 3 || apiKey.length > 250) {
        toastr.error("apiKey length should be between minlength 3 and maxlength 250");
        return false;
    }
    else if (!apiKey.match(Addressegex)) {
        toastr.error("apiKey must start with alphabetic characters");
        return false;
    }
    else if (clientId == '') {
        toastr.error("clientId field is required");
        return false;
    }
    else if (clientId.length < 3 || clientId.length > 250) {
        toastr.error("clientId length should be between minlength 3 and maxlength 250");
        return false;
    }
    else if (!clientId.match(Addressegex)) {
        toastr.error("clientId must start with alphabetic characters");
        return false;
    }
    else if (clientSecret == '') {
        toastr.error("clientSecret field is required");
        return false;
    }
    else if (clientSecret.length < 3 || clientSecret.length > 250) {
        toastr.error("clientSecret length should be between minlength 3 and maxlength 300");
        return false;
    }
    else if (!clientSecret.match(Addressegex)) {
        toastr.error("clientSecret must start with alphabetic characters");
        return false;
    }
    else if (allowLimit == '') {
        toastr.error("allowLimit field is required");
        return false;
    }
    else if (allowLimit.length < 1 || description.length > 8) {
        toastr.error("allowLimit length should be between minlength 1 and maxlength 8");
        return false;
    }
    else if (!allowLimit.match(numbersregex)) {
        toastr.error("allowLimit must start with Numbers");
        return false;
    }
    else if (service == '') {
        toastr.error("service field is required");
        return false;
    }
    else if (service.length < 3 || service.length > 250) {
        toastr.error("service length should be between minlength 3 and maxlength 300");
        return false;
    }
    else if (!service.match(Addressegex)) {
        toastr.error("service must start with alphabetic characters");
        return false;
    }
    else if (apiHost == '') {
        toastr.error("apiHost field is required");
        return false;
    }
    else if (apiHost.length < 3 || apiHost.length > 250) {
        toastr.error("apiHost length should be between minlength 3 and maxlength 250");
        return false;
    }
    else if (!apiHost.match(Addressegex)) {
        toastr.error("apiHost must start with alphabetic characters");
        return false;
    }
    else if (consumedLimit == '') {
        toastr.error("consumedLimit field is required");
        return false;
    }
    else if (consumedLimit.length < 1 || consumedLimit.length > 8) {
        toastr.error("consumedLimit length should be between minlength 1 and maxlength 8");
        return false;
    }
    else if (!consumedLimit.match(numbersregex)) {
        toastr.error("consumedLimit must start with alphabetic characters");
        return false;
    }
    else if (status == '') {
        toastr.error("status field is required");
        return false;
    }
    else if (status.length < 3 || status.length > 250) {
        toastr.error("status length should be between minlength 3 and maxlength 250");
        return false;
    }
    else if (!status.match(Addressegex)) {
        toastr.error("status must start with alphabetic characters");
        return false;
    }
    else if (password == '') {
        toastr.error("password field is required");
        return false;
    }
    else if (password.length < 6 || description.length > 30) {
        toastr.error("password length should be between minlength 6 and maxlength 30");
        return false;
    }
}

function addUpdateProject() {
    let title = $('#title').val();
    let description = $('#description').val();
    let url = $('#url').val();
    let status = $('#status').val();

    var Addressegex = /^[a-zA-Z][a-zA-Z0-9\s.,#-]*$/;
    
    $(".error").remove();

    if (title == '') {
        toastr.error("title field is required");
        return false;
    }
    else if (title.length < 3 || title.length > 50) {
        toastr.error("Title length should be between minlength 3 and maxlength 50");
        return false;
    }
    else if (!title.match(Addressegex)) {
        toastr.error("Title must start with alphabetic characters");
        return false;
    }
    else if (description == '') {
        toastr.error("description field is required");
        return false;
    }
    else if (description.length < 3 || description.length > 300) {
        toastr.error("description length should be between minlength 3 and maxlength 300");
        return false;
    }
    else if (!description.match(Addressegex)) {
        toastr.error("description must start with alphabetic characters");
        return false;
    }
    else if (url == '') {
        toastr.error("url field is required");
        return false;
    }
    else if (status == '') {
        toastr.error("status field is required");
        return false;
    }
}
















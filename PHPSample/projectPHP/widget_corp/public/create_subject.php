<!--session should be at the top!-->
<?php require_once("../includes/session.php"); ?>

<?php require_once("../includes/db_connection.php"); ?>
<?php require_once("../includes/functions.php"); ?>

<?php require_once("../includes/validation_functions.php"); ?>

<?php
if (isset($_POST['submit'])) {
  //Process the form

  //code import from BasicPHP/Database/databases_insert.php

  // Often these are form values in $_POST
  $menu_name = $_POST["menu_name"];
  //put typecase just in case. user input string with SQL injection
  $position = (int) $_POST["position"];
  $visible = (int) $_POST["visible"];

  // Escape all strings. it prevent basic SQL injection
  // use it before create query statement
  $menu_name = mysql_prep($menu_name);

  $required_fields = array("menu_name", "position", "visible");
  validate_presences($required_fields);

  $fields_with_max_lengths = array("menu_name" => 30);
  validate_max_lengths($fields_with_max_lengths);

  if(!empty($errors)){
    $_SESSION["errors"] = $errors;
    redirect_to("new_subject.php");
  }

  // 2. Perform database query
  $query  = "INSERT INTO subjects (";
  $query .= "  menu_name, position, visible";
  $query .= ") VALUES (";
  $query .= "  '{$menu_name}', {$position}, {$visible}";
  $query .= ")";

  $result = mysqli_query($connection, $query);

  if ($result) {
    // Success
    $_SESSION["message"] = "Subject creation successed.";
    redirect_to("manage_content.php");
  } else {
    // Failure
    $_SESSION["message"] = "Subject creation failed.";
    redirect_to("new_subject.php");
  }

  //'mysqli_insert_id()' returns id of most recent data
} else {
  //This is probably a GET request
  redirect_to("new_subject.php");
}

?>



<?php
	if (isset($connection)) { mysqli_close($connection);}
?>

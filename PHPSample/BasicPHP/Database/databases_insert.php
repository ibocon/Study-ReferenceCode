<?php
  // 1. Create a database connection
  $dbhost = "localhost";
  $dbuser = "root";
  $dbpass = "wlfka102";
  $dbname = "widget_corp";
  $connection = mysqli_connect($dbhost, $dbuser, $dbpass, $dbname);
  // Test if connection succeeded
  if(mysqli_connect_errno()) {
    die("Database connection failed: " .
         mysqli_connect_error() .
         " (" . mysqli_connect_errno() . ")"
    );
  }
?>

<?php
	// Often these are form values in $_POST
	$menu_name = "Today's Widget Trivia";
  //put typecase just in case. user input string with SQL injection
	$position = (int) 4;
	$visible = (int) 1;

	// Escape all strings. it prevent basic SQL injection
  // use it before create query statement
	$menu_name = mysqli_real_escape_string($connection, $menu_name);

	// 2. Perform database query
	$query  = "INSERT INTO subjects (";
	$query .= "  menu_name, position, visible";
	$query .= ") VALUES (";
	$query .= "  '{$menu_name}', {$position}, {$visible}";
	$query .= ")";

	$result = mysqli_query($connection, $query);

	if ($result) {
		// Success
		// redirect_to("somepage.php");
		echo "Success!";
	} else {
		// Failure
		// $message = "Subject creation failed";
    //find recent error from 'connection'
		die("Database query failed. " . mysqli_error($connection));
	}
  //'mysqli_insert_id()' returns id of most recent data
?>

<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Databases</title>
	</head>
	<body>

	</body>
</html>

<?php
  // 5. Close database connection
  mysqli_close($connection);
?>

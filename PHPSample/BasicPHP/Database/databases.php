<?php
  // 1. Create a database connection
  $dbhost = "localhost";
  $dbuser = "root";
  $dbpass = "wlfka102";
  $dbname = "widget_corp";
  //handle for connction = 'connection'
  $connection = mysqli_connect($dbhost, $dbuser, $dbpass, $dbname);

  // Test if connection succeeded
  //'mysqli_connect_errno' checks error number
  if(mysqli_connect_errno()) {
    //'die' force quit PHP with a message
    //'mysqli_connect_error' returns a string
    die("Database connection failed: " .
         mysqli_connect_error() .
         " (" . mysqli_connect_errno() . ")"
    );
  }
?>

<?php
	// 2. Perform database query
  //no need put ; at the end of query
	$query  = "SELECT * ";
	$query .= "FROM subjects "; //need space!
	$query .= "WHERE visible = 1 ";
	$query .= "ORDER BY position ASC";
	$result = mysqli_query($connection, $query);
	// Test if there was a query error
	if (!$result) {
		die("Database query failed.");
	}
?>

<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Databases</title>
	</head>
	<body>

		<ul>
		<?php
			// 3. Use returned data (if any)
      $choice = 1;

      if($choice === 0){
        //'while' can be only syntax used for 'mysqli_fetch_row'
        while($row = mysqli_fetch_row($result)){
          var_dump($row);
          echo "<hr/>";
        }
      }
      else if($choice === 1){
        while($subject = mysqli_fetch_assoc($result)) {
          // output data from each row
      ?>
          <li><?php echo $subject["menu_name"] . " (" . $subject["id"] . ")"; ?></li>
      <?php
        }
      }

		?>
		</ul>

		<?php
		  // 4. Release returned data
		  mysqli_free_result($result);
		?>
	</body>
</html>

<?php
  // 5. Close database connection
  mysqli_close($connection);
?>

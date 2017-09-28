<?php
  // Sessions use cookies which use headers.
	// Must start before any HTML output
	// unless output buffering is turned on.

  //'session' is stored inside of web server
  session_start();
?>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Sessions</title>
	</head>
	<body>

		<?php
			$_SESSION["first_name"] = "Kevin";
			$name = $_SESSION["first_name"];
			echo $name;
		?>

	</body>
</html>

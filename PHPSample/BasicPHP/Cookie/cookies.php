<?php

// REMEMBER: Setting cookies must be before *any* HTML output
//           unless output buffering is turned on.
	$name = "test";
	$value = "hello";
	//'time()' returns current time of server
	$expire = time() + (60*60*24*7); // add seconds
	setcookie($name, $value, $expire);

	// Three ways to unset cookies:
	// 1. setcookie($name);
	// 2. setcookie($name, null, $expire);
	// 3. setcookie($name, $value, time() - 3600);

	// The recommendation for unsetting:
	// setcookie($name, null, time() - 3600);

?>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Cookies</title>
	</head>
	<body>

		<?php
		//how to get '_COOKIE' value
		$test = isset($_COOKIE["test"]) ? $_COOKIE["test"] : "";
		echo $test;
		?>

	</body>
</html>

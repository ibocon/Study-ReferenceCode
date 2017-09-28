<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Constants</title>
	</head>
	<body>

		<?php
			$max_width = 980;
      //how to create constant
			define("MAX_WIDTH", 980);
			echo MAX_WIDTH;
		?>
		<br />
		<?php
      // can't change the value
      //ERROR
		    MAX_WIDTH = MAX_WIDTH + 1
		    echo MAX_WIDTH;
		?>

		<?php
      // can't even redefine it
      //ERROR
		    define("MAX_WIDTH", 981);
		    echo MAX_WIDTH;
		?>

	</body>
</html>

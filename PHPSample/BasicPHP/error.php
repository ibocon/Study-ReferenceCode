<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Error Report</title>
	</head>
	<body>

		<?php
      /*
        in php.in file
        display_errors = On
        error_reporting = E_ALL
      */
      ini_set("display_errors", "On");
      error_reporting(E_ALL);

      error_reporting(E_ALL | E_STRICT);
      //Use ~ for "omit"
      error_reporting(E_ALL & ~E_DEPRECATED);
      //return the current level
      error_reporting();
      /*
      http://php.net/manual/en/errorfunc.constants.php

        Fatal Errors
        Syntax Errors
        Warnings
        Notices

      error is recored on 'error_log'
      */
    ?>
	</body>
</html>

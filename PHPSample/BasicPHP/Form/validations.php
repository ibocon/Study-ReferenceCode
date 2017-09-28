<!DOCTYPE html>

<html lang="en">
	<head>
		<title>Validations</title>
	</head>
	<body>

		<?php
    /*
      presence
      string length
      type
      inclusion in a set
      uniqueness
      format
    */

		// * presence
		// use trim() so empty spaces don't count
		// use === to avoid false positives
		// empty() would consider "0" to be empty
		$value = trim("0");
    //presence of 'value'
		if (!isset($value) || $value === "") {
			echo "Validation failed.<br />";
		}

		// * string length
		// minimum length
		$value = "abcd";
		$min = 3;
    //'strlen' count string length
		if (strlen($value) < $min) {
			echo "Validation failed.<br />";
		}
		// max length
		$max = 6;
		if (strlen($value) > $max) {
			echo "Validation failed.<br />";
		}

		// * type
		$value = "1";
    //'is_string' checks type of variable
		if (!is_string($value)) {
			echo "Validation failed.<br />";
		}

		// * inclusion in a set
    //default type from user is String
		$value = "1";
		$set = array("1", "2", "3", "4");
    //'in_array' checks 'value' in 'set'
		if (!in_array($value, $set)) {
			echo "Validation failed.<br />";
		}

		// * uniqueness
		// uses a database to check uniqueness

		// * format
		// use a regex on a string
		// preg_match($regex, $subject) find matched string insde
		if (preg_match("/PHP/", "PHP is fun.")) {
			echo "A match was found.";
		} else {
		  echo "A match was not found.";
		}

		$value = "nobody@nowhere.com";
		// preg_match is most flexible
		if (!preg_match("/@/", $value)) {
			echo "Validation failed.<br />";
		}
		// strpos is faster than preg_match
		// use === to make exact match with false
		if (strpos($value, "@") === false) {
		  echo "Validation failed.<br />";
		}

		?>

	</body>
</html>

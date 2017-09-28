<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Functions: Multiple Returns</title>
	</head>
	<body>

		<?php
			//use array to return multi vars
			function add_subt($val1, $val2) {
			  $add = $val1 + $val2;
			  $subt = $val1 - $val2;
			  return array($add, $subt);
			}

			$result_array = add_subt(10,5);
			//use the result data by using index
			echo "Add: " . $result_array[0] . "<br />";
			echo "Subt: " . $result_array[1] . "<br />";

			//how to define the key for each index
			//it helps to give meaning to reuslt
			list($add_result, $subt_result) = add_subt(20,7);
			echo "Add: " . $add_result . "<br />";
			echo "Subt: " . $subt_result . "<br />";

		?>

	</body>
</html>

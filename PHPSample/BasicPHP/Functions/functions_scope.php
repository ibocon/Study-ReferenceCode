<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Functions: Scope</title>
	</head>
	<body>
		<?php
      // global scope
      //it can be used everywhere
			$bar = "outside";

			function foo() {
        //how to use global var inside of func
				global $bar;
				if (isset($bar)) {
					echo "foo: " . $bar . "<br />";
				}
        //it change the global var
				$bar = "inside";
			}

			echo "1: " . $bar . "<br />";
			foo();
			echo "2: " . $bar . "<br />";
		?>

	</body>
</html>

<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Array Functions</title>
	</head>
	<body>
    <?php
      $var = "string";
      //variable value
      echo $var;
      //print readable array
      print_r($array);
      //variable type
      gettype($var);
      //variable type and value
      var_dump($var);
      //array of defined variables
      get_defined_vars();
      //show backtrace
      debug_backtrace();
    ?>

    <?php
			$number = 99;
			$string = "Bug?";
			$array = array(1 => "Homepage", 2 => "About Us", 3 => "Services");

			var_dump($number);
			var_dump($string);
			var_dump($array);

		?>
		<br />
		<pre>
		<?php
			print_r(get_defined_vars());
		?>
		</pre>
		<br />
		<?php

			function say_hello_to($word) {
		    echo "Hello {$word}!<br />";
				var_dump(debug_backtrace());
			}

			say_hello_to('Everyone');
      //Xdebug - http://xdebug.org/
      //DBG & FirePHP
		?>
	</body>
</html>

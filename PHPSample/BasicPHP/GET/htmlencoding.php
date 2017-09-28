<!DOCTYPE html>
<html lang="en">
  <head>
    <title>HTML encoding</title>
  </head>
  <body>

    <?php
    //HTML has reserved char : <(&lt;) >(&gt;) &(&amp;) "(&quot;)
      $linktext = "<Click> & learn more";
    ?>

    <a href="">
      <!--'htmlspecialchars' encodes HTML's reserved char-->
      <?php echo htmlspecialchars($linktext); ?>
    </a>
    <br />

    <?php
      //speical characters
      $text = "™£•“—é";
      //encode speical characters into HTML entities
      echo htmlentities($text);

    ?>

    <br />
    <?php // What to use when

    $url_page = "php/created/page/url.php";
    $param1 = "This is a string with < >";
    $param2 = "&#?*$[]+ are bad characters";
    $linktext = "<Click> & learn more";

    $url = "http://localhost/";
    //http://localhost/
    $url .= rawurlencode($url_page);
    //http://localhost/php/created/page/url.php
    $url .= "?" . "param1=" . urlencode($param1);
    //http://localhost/php/created/page/url.php?param1=&#?*$[]+ are bad characters
    $url .= "&" . "param2=" . urlencode($param2);
    //http://localhost/php/created/page/url.php?param1=&#?*$[]+ are bad characters&param2=<Click> & learn more
    ?>

    <a href="<?php echo htmlspecialchars($url); ?>">
      <?php echo htmlspecialchars($linktext); ?>
    </a>

  </body>
</html>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
   "http://www.w3.org/TR/html4/loose.dtd">

<html lang="en">
  <head>
    <title>urlencode</title>
  </head>
  <body>

    <?php
    //Reserved Characters in URLs
    // ! # $ % & ' ( ) * + , / : ; = ? @ [ ]
      $page = "William Shakespeare";
      $quote = "To be or not to be";
      //'rawurlencode' encodes reserved char to hex, and also space becomes %20.
      //usually, used for filesystem
      $link1 =  "/bio/" . rawurlencode($page) . "?quote=" . urlencode($quote);
      //'urlencode' encodes reserved char to hex.
      $link2 =  "/bio/" . urlencode($page) . "?quote=" . rawurlencode($quote);

      echo $link1 . "<br />";
      echo $link2 . "<br />";
    ?>

  </body>
</html>

<?php if(!isset($layout_context)){
  $layout_context = "public";
} ?>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Widget Corp<?php if($layout_context == "admin"){echo " Admin";}?></title>
    <meta charset="utf-8"/>
		<link href="stylesheets/public.css" media="all" rel="stylesheet" type="text/css" />
	</head>
	<body>
    <div id="header">
      <h1>Widget Corp<?php if($layout_context == "admin"){echo " Admin";}?></h1>
    </div>

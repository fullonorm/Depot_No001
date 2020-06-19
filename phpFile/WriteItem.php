<?php
include "Connect.php";
$UID = $_SESSION['UID'];
$NICKNAME = $_SESSION['NICKNAME'];
$ITEMID = $_POST['ITEMID'];
$String = $_POST['WInput'];

//mysqli_select_db($con,'UserInfo');

$UpdateItemSql = "update UserInfo set ITEMID = '$String' where NICKNAME = '$NICKNAME'";
$UpdateItem = mysqli_query($con,$UpdateItemSql);

?> 

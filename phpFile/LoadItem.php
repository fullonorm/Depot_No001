<?php
include 'Connect.php';
$UID = $_SESSION['UID'];
$NICKNAME = $_SESSION['NICKNAME'];
$ITEMID = $_POST['ITEMID'];

//mysqli_select_db($con,'UserInfo');
$LoadItemSql = "select ITEMID from UserInfo where NICKNAME = '$NICKNAME'";
$LoadItem = mysqli_query($con,$LoadItemSql);
$rowItem = mysqli_fetch_assoc($LoadItem);

echo $rowItem['ITEMID'];

?>


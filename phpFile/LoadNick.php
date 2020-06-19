<?php
include "Connect.php";
$UID = $_SESSION['UID'];
$NICKNAME = $_SESSION['NICKNAME'];


$insertsql = "insert into UserInfo(UID,NICKNAME) values('$UID','$NICKNAME')";
$insert = mysqli_query($con,$insertsql);

$LoadNickSql = "select NICKNAME from UserInfo where UID = '$UID'";
$LoadNick = mysqli_query($con,$LoadNickSql);
$rowNick = mysqli_fetch_array($LoadNick);

if($rowNick != 0){
echo $rowNick['NICKNAME'];
}
?>

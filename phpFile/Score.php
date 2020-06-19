<?php
include 'Connect.php';

$NICKNAME = $_SESSION['NICKNAME'];
$Score = $_POST['Score'];

mysqli_select_db($con,'UserInfo');

$LoadScoreSql = "select SCORE from UserInfo where NICKNAME = '$NICKNAME'";
$LoadScore = mysqli_query($con,$LoadScoreSql);
$rowScore = mysqli_fetch_assoc($LoadScore);

//echo $rowScore['SCORE'];

$result = $rowScore['SCORE'] + $Score;

$UpdateScoreSql = "update UserInfo set SCORE = '$result' where NICKNAME = '$NICKNAME'";
mysqli_query($con,$UpdateScoreSql); 
echo $result;
 

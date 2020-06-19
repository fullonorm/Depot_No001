<?php
include 'Connect.php';
$UID = $_SESSION['UID'];
$LoadEmail = $_SESSION['LoginEmail'];
$NICKNAME = $_POST['NICKNAME'];
$UID = $_SESSION['UID'];

//mysqli_select_db($con,"UserLogin");

$addNickSql = "select * from UserLogin where NICKNAME = '$NICKNAME'";
$check = mysqli_query($con,$addNickSql);

$sql = "update UserLogin set NICKNAME = '$NICKNAME' where EMAIL = '$LoadEmail'";
$result = mysqli_query($con,$sql);

if(empty($NICKNAME)){
echo 'Fail';
}
elseif($check -> num_rows >= 1){
echo 'Exist';
}

elseif($result){
$LoadNickSql = "select NICKNAME from UserLogin where EMAIL = '$LoadEmail'";
$LoadNick = mysqli_query($con,$LoadNickSql);
$LoadNickrows = mysqli_fetch_assoc($LoadNick);
if($LoadNickrows != 0){
$NICKNAME = $LoadNickrows['NICKNAME'];
$_SESSION['NICKNAME'] = $NICKNAME;
echo 'Success';
} 
}

?>

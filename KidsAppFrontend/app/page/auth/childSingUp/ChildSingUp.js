import { View, Text, SafeAreaView } from 'react-native';
import React, { useState } from 'react';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input'
import AuthTextButton from '@/app/components/authTextButton'
import Icon from '@/app/components/icon'
import styles from './styles'

const ChildSingUp = () => {
  const [user, setUser] = useState("");
  const [mail, setMail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  function ParentSingUp() {
    console.log("Navigating to Parent Sign Up...");
  }

  function forgotPassword() {

  }
  function goSingIn() {

  }
  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder={'Username'}
          onChangeText={setUser}
          value={user}
          iconName={'user'}
        />
        <Input
          placeholder={'E-Mail'}
          onChangeText={setMail}
          value={mail}
          iconName={'mail'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setPassword}
          value={password}
          iconName={'key'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setConfirmPassword}
          value={confirmPassword}
          iconName={'key'}
        />
        <View style={styles.forgotPassword}>
          <AuthTextButton
            text={'Forgot Password?'}
            onPress={forgotPassword} />
        </View>
        <Button
          onPress={ParentSingUp}
          ButtonName={"Sign Up"}
        />
        <AuthTextButton
          text={`I have an account! Sing In`}
          onPress={goSingIn} />
      </SafeAreaView>

    </Background>
  );
};

export default ChildSingUp;

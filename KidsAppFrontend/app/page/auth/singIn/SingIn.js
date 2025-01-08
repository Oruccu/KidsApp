import {  View, SafeAreaView } from 'react-native'
import React, { useState } from 'react';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input'
import AuthTextButton from '@/app/components/authTextButton'
import styles from './styles'

const SingIn = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  function ParentSingUp() {
    console.log("Navigating to Parent Sign Up...");
  }

  function goSingIn() {

  }
  function forgotPassword() {

  }
  
  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Input
          placeholder={'Username'}
          onChangeText={setUsername}
          value={username}
          iconName={'user'}
        />
        <Input
          placeholder={'Password'}
          onChangeText={setPassword}
          value={password}
          iconName={'key'}
        />
        <View style={styles.forgotPassword}>
          <AuthTextButton
            text={'Forgot Password?'}
            onPress={forgotPassword} />
        </View>
        <Button
          onPress={ParentSingUp}
          ButtonName={"Sign In"}
        />
        <AuthTextButton
          text={`Don’t have  account yet? Sing Up`}
          onPress={goSingIn} />
      </SafeAreaView>

    </Background>
    )
}

export default SingIn


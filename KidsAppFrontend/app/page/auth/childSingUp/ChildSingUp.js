import React, { useState, useEffect } from 'react';
import { View, Alert, ActivityIndicator, SafeAreaView, Text } from 'react-native';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input';
import AuthTextButton from '@/app/components/authTextButton';
import styles from './styles';
import { registerChild, getParentUsers } from '@/app/services/api';
import { useNavigation } from '@react-navigation/native';

const ChildSignUp = () => {
  const navigation = useNavigation();
  const [parents, setParents] = useState([]);
  const [loadingParents, setLoadingParents] = useState(false);

  useEffect(() => {
    fetchParentUsers();
  }, []);

  const fetchParentUsers = async () => {
    setLoadingParents(true);
    try {
      const data = await getParentUsers();
      setParents(data);
    } catch (error) {
      Alert.alert('Error', error.message);
    } finally {
      setLoadingParents(false);
    }
  };

  const ChildSignUpSchema = Yup.object().shape({
    username: Yup.string()
      .min(3, 'Username must be at least 3 characters')
      .required('Please enter your username'),
    email: Yup.string()
      .email('Invalid email')
      .required('Please enter your email'),
    password: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Please enter your password'),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref('password'), null], 'Passwords do not match')
      .required('Please confirm your password'),
    parentUserId: Yup.string()
      .required('Please select a parent'),
  });

  const handleChildSignUp = async (values, { setSubmitting, resetForm }) => {
    try {
      const response = await registerChild(
        values.email,
        values.password,
        values.username,
        parseInt(values.parentUserId)
      );
      if (response.IsSucced) {
        Alert.alert('Success', response.Message);
        resetForm();
        navigation.navigate('SignIn');
      } else {
        Alert.alert('Error', response.Message);
      }
    } catch (error) {
      Alert.alert('Error', error.message);
    } finally {
      setSubmitting(false);
    }
  };

  const forgotPassword = () => {
    Alert.alert('Info', 'Forgot Password functionality will be implemented here.');
  };

  const goSignIn = () => {
    navigation.navigate('SignIn');
  };

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Formik
          initialValues={{
            username: '',
            email: '',
            password: '',
            confirmPassword: '',
            parentUserId: '',
          }}
          validationSchema={ChildSignUpSchema}
          onSubmit={handleChildSignUp}
        >
          {({
            handleChange,
            handleBlur,
            handleSubmit,
            values,
            errors,
            touched,
            isSubmitting,
            setFieldValue,
          }) => (
            <View>
              <Input
                placeholder={'Username'}
                onChangeText={handleChange('username')}
                onBlur={handleBlur('username')}
                value={values.username}
                iconName={'user'}
              />
              {errors.username && touched.username && (
                <Text style={styles.errorText}>{errors.username}</Text>
              )}

              <Input
                placeholder={'E-Mail'}
                onChangeText={handleChange('email')}
                onBlur={handleBlur('email')}
                value={values.email}
                iconName={'mail'}
                keyboardType="email-address"
                autoCapitalize="none"
              />
              {errors.email && touched.email && (
                <Text style={styles.errorText}>{errors.email}</Text>
              )}

              <Input
                placeholder={'Password'}
                onChangeText={handleChange('password')}
                onBlur={handleBlur('password')}
                value={values.password}
                iconName={'key'}
                secureTextEntry
              />
              {errors.password && touched.password && (
                <Text style={styles.errorText}>{errors.password}</Text>
              )}

              <Input
                placeholder={'Confirm Password'}
                onChangeText={handleChange('confirmPassword')}
                onBlur={handleBlur('confirmPassword')}
                value={values.confirmPassword}
                iconName={'key'}
                secureTextEntry
              />
              {errors.confirmPassword && touched.confirmPassword && (
                <Text style={styles.errorText}>{errors.confirmPassword}</Text>
              )}

              <View style={styles.forgotPassword}>
                <AuthTextButton
                  text={'Forgot Password?'}
                  onPress={forgotPassword}
                />
              </View>
                <Button onPress={handleSubmit} ButtonName={"Sign Up"} />
              <AuthTextButton
                text={`I have an account! Sign In`}
                onPress={goSignIn}
              />
            </View>
          )}
        </Formik>
      </SafeAreaView>
    </Background>
  );
};

export default ChildSignUp;

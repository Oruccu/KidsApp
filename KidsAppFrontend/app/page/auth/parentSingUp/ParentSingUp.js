import React from 'react';
import { View, Alert, ActivityIndicator, SafeAreaView, Text } from 'react-native';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Background from '@/app/components/background';
import Button from '@/app/components/button';
import Input from '@/app/components/input';
import AuthTextButton from '@/app/components/authTextButton';
import styles from './styles';
import { registerParent } from '@/app/services/api';
import { useNavigation } from '@react-navigation/native';

const ParentSignUp = () => {
  const navigation = useNavigation();

  const ParentSignUpSchema = Yup.object().shape({
    email: Yup.string()
      .email('Invalid email')
      .required('Please enter your email'),
    password: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Please enter your password'),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref('password'), null], 'Passwords do not match')
      .required('Please confirm your password'),
  });

  const handleParentSignUp = async (values, { setSubmitting, resetForm }) => {
    try {
      const response = await registerParent(values.email, values.password);
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

  const goSignIn = () => {
    navigation.navigate('SignIn');
  };

  return (
    <Background>
      <SafeAreaView style={styles.container}>
        <Formik
          initialValues={{ email: '', password: '', confirmPassword: '' }}
          validationSchema={ParentSignUpSchema}
          onSubmit={handleParentSignUp}
        >
          {({
            handleChange,
            handleBlur,
            handleSubmit,
            values,
            errors,
            touched,
            isSubmitting,
          }) => (
            <View>
              <Input
                placeholder={'Parent Email'}
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
                placeholder={'Parent Password'}
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

              {isSubmitting ? (
                <ActivityIndicator size="large" color="#0000ff" />
              ) : (
                <Button onPress={handleSubmit} ButtonName={"Sign Up"} />
              )}

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

export default ParentSignUp;

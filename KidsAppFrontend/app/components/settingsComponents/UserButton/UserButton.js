// src/app/components/UserButton.js
import React from 'react';
import { TouchableOpacity, Text } from 'react-native';
import styles from './styles'; 

const UserButton = ({ onPress, title, buttonBackgroundColor, customStyle }) => {
  return (
    <TouchableOpacity
      onPress={onPress}
      style={[
        styles.container,
        { backgroundColor: buttonBackgroundColor },
        customStyle,
      ]}
    >
      <Text style={styles.btnName}>{title}</Text>
    </TouchableOpacity>
  );
};

export default UserButton;

import React from 'react';
import { TouchableOpacity, Text } from 'react-native';
import styles from './styles';
import useTheme from '@/app/hooks/useTheme';

const SettingsButton = ({ onPress, buttonTitle, theme, isActive }) => {
  const themeColors = useTheme();

  return (
    <TouchableOpacity
      onPress={onPress}
      style={[
        styles[theme].container,
        isActive && { borderWidth: 2, borderColor: themeColors.Primary },
      ]}
    >
      <Text style={styles[theme].title}>{buttonTitle}</Text>
    </TouchableOpacity>
  );
};

export default SettingsButton;

import { View, Text, TextInput } from 'react-native'
import React from 'react'
import styles from './styles'
import color from '@/app/styles/color'
import { AntDesign } from '@expo/vector-icons'
const Input = ({ placeholder, onChangeText, value, iconName }) => {
  return (
    <View style={styles.container}>
      <View style={styles.icon}>
        <AntDesign
          name={iconName}
          size={20}
          color={color.BoyColors.iconColor} />
      </View>
      <View>
        <TextInput
          placeholder={placeholder}
          onChangeText={onChangeText}
          value={value}
          style={styles.input}
          placeholderTextColor={color.BoyColors.textColor}

        />
      </View>
    </View>
  )
}

export default Input
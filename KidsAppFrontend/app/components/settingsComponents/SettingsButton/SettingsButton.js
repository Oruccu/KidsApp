import { View, Text, TouchableOpacity } from 'react-native'
import React from 'react'
import styles from './styles'
const SettingsButton = ({onPress, buttonTitle, theme }) => {
  return (
    <TouchableOpacity onPress={onPress}>
      <View style={styles[theme].container}>
        <Text style={styles[theme].title}>{buttonTitle}</Text>
      </View>
    </TouchableOpacity>
  )
}

export default SettingsButton
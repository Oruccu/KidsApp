import { View, Text, TouchableOpacity } from 'react-native'
import React from 'react'
import styles from './styles'
const Button = ({onPress, ButtonName}) => {
  return (
    <TouchableOpacity onPress={onPress}>
      <View style={styles.container}>
        <Text style={styles.btnName}>{ButtonName}</Text>
      </View>
    </TouchableOpacity>
  )
}

export default Button
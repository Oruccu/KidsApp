import { View, Text, TouchableOpacity } from 'react-native'
import React from 'react'
import styles from './styles'
import { BlurView } from 'expo-blur';

const AuthTextButton = ({ text, onPress }) => {
  return (
    <TouchableOpacity onPress={onPress}>
      <View style={styles.container}>
        <BlurView
          style={styles.blurContainer}
          blurType="xlight"
          blurAmount={2}>
          <Text style={styles.text}>{text}</Text>
        </BlurView>

      </View>
    </TouchableOpacity >
  )
}

export default AuthTextButton
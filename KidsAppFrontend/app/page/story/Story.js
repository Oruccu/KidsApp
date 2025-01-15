import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/Banner'
import ListenToStory from '@/app/components/homeComponent/ListenToStory'
import { useNavigation } from '@react-navigation/native';

const Story = () => {
  return (
    <Background showOverlay={true}>
      <SafeAreaView>
        <View>

      <Banner bannerTitle={'Time'} />
        </View>
        <View>
        <Text>Hikaye</Text>
        </View>
        <View>
          
        </View>

      </SafeAreaView>
    </Background>
  )
}

export default Story
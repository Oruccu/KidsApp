import { View, Text, SafeAreaView } from 'react-native'
import React from 'react'
import Background from '@/app/components/background'
import styles from './styles'
import Banner from '@/app/components/Banner'
import ListenToStory from '@/app/components/homeComponent/ListenToStory'
import { useNavigation } from '@react-navigation/native';
import { getAudioBookById } from '@/app/services/api'; 
import { playAudioFromUrl } from '@/app/utils/playAudioFromUrl'; 
import ContinueToListen from '@/app/components/homeComponent/ContinueToListen'
import Game from '@/app/components/homeComponent/Game'

const Home = () => {
  const navigation = useNavigation();

  async function ListenStory() {
    try {
      const audioBook = await getAudioBookById(1);
      console.log('audioBook:', audioBook);
      if (audioBook?.audioFileUrl) {
        console.log(audioBook.audioFileUrl);
        await playAudioFromUrl(audioBook.audioFileUrl);
      }
    } catch (error) {
      console.error(error);
    }
  }
  function listenToStory(){
    
  }
  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle={85} />
        <View>
          <ContinueToListen onPress={listenToStory}/>
        </View>
        <View style={styles.storyContainer}>
          <ListenToStory Title={"The Little Prince"} onPress={ListenStory} url={require(`@/app/assets/icon/prince.png`)}/>
          <ListenToStory Title={"Snow White"} onPress={ListenStory} url={require(`@/app/assets/icon/snowWhite.png`)}/>
        </View>
        <View>
          <View>
            <Game/>
          </View>
        </View>
      </SafeAreaView>
    </Background>
  )
}

export default Home
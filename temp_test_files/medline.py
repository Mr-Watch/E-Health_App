from bs4 import BeautifulSoup
from requests import get
import json
import xmltodict

def remove_html_tags(text):
    import re
    clean = re.compile('<.*?>')
    return re.sub(clean, '', text)

data = {}

search_argument = 'asthma'#input('Enter a search term ')
response = get('https://wsearch.nlm.nih.gov/ws/query?db=healthTopics&retmax=1&term='+search_argument)



response_soup = BeautifulSoup(response.content, 'lxml')
print(remove_html_tags(response_soup))
#print(response_soup.find('term').text)

'''
data['search_term'] = response_soup.find('term').text
data['url'] = response_soup.find('document').get('url')
data['titles'] = response_soup.find('content', {'name' : 'title'})
data['full_summares'] = response_soup.find('content', {'name' : 'FullSummary'})


#json_data = json.dumps(data)

#print(json_data)

#print(data)

print(response_soup.find_all('content', {'name' : 'altTitle'}))


#dict_data = xmltodict.parse(response.text)

#print(json.dumps(dict_data))

term = dict_data['nlmSearchResult']["term"]
url = dict_data['nlmSearchResult']['list']['document']['@url']
title = ''
for data_entires in dict_data['nlmSearchResult']['list']['document']['content']:
    title = assigner(data_entires,title)
    print(title)



json = '"term":"{}", "url":"{}", "title":{}, "altTitle":{}, "organizationName":"{}", "FullSummary":"{}", "groupName":"{}"'

print(title)

article_processed = BeautifulSoup(article,'lxml').text

#print(dict['@name'])

with open('/home/sarantis/Desktop/test.json', 'w') as json_file:
    json.dump(dict, json_file)
    json_file.close()
    '''